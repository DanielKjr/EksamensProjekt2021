using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace EksamensProjekt2021
{
    public class RoomManager
    {
        private enum RoomType { Empty, Cleared, Normal, Hard, Loot, Boss }
        private List<string> indexList = new List<string>(20);
        public static byte[,] roomLayout = new byte[5, 5];
        public static byte[,] roomStyle = new byte[5, 5]; //0-9
        public static bool[,] revealedRoom = new bool[5, 5];
        public static byte[] playerInRoom = new byte[2]; //See which room the player is in. (X, Y)
        public static int roomsCleared; //----------------------------------------------------------------------------------------------------IMPLEMENT
        public static int levelsCleared;
        public static byte mapReruns = 0; //See how many times the RoomsGenerator needed to run
        private Texture2D[] floor = new Texture2D[3];
        private Texture2D wall;

        private byte filledRooms = 0;
        private byte failSafe = 0;
        private int[] index = new int[2];
        private Random rnd = new Random();

        /// <summary>
        /// Generates the map based on amount of rooms desired.
        /// Will auto retry if it fails (Never fails tho)
        /// </summary>
        /// <param name="amountOfRooms"></param>
        public void CreateMap(byte amountOfRooms)
        {
            Reset();

            while (filledRooms < amountOfRooms)
            {
                for (sbyte i = -1; i < 2; i += 2)
                {
                    RoomCreate(i, 0, index[0], index[1], amountOfRooms); //Check x=-1 and x=1
                    RoomCreate(0, i, index[0], index[1], amountOfRooms); //Check y=-1 and y=1
                }

                if (indexList.Count > 0) //Finds next random index point. Cannot be already used.
                {
                    int listIndex = rnd.Next(0, indexList.Count); //Creates random indexPos
                    string parseString = indexList[listIndex]; //Fill list contents into string
                    indexList.RemoveAt(listIndex); //Removes entry from list

                    index[0] = int.Parse(parseString[0] + "0") / 10; //Converts list entry into int for index pos
                    index[1] = int.Parse(parseString[1] + "0") / 10; //Force to add "0" for it to be string. Parse doesnt take char.
                }
                failSafe++;
                if (failSafe > 100) Reset();

            }
            roomLayout[index[0], index[1]] = 5; //Set last created room to be boss room
            RevealRooms();
            Debug(failSafe, mapReruns);
        }
        /// <summary>
        /// Creates rooms in the map. Checks if there can be, should be, and then calls Chance()
        /// Adds entry to indexList for further map creation.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <param name="rooms"></param>
        private void RoomCreate(sbyte x, sbyte y, int ix, int iy, int rooms)
        {
            if (ix + x > -1 && ix + x < 5 && iy + y > -1 && iy + y < 5) //Out of bounds checker
            {
                if (roomLayout[ix + x, iy + y] == 0 && filledRooms <= rooms) //Checks if there already a room, and if there should be added more rooms (desired amount)
                {
                    if ((byte)rnd.Next(0, 2) == 1) //Randomizes if there should be a room
                    {
                        roomLayout[ix + x, iy + y] = (byte)Chance();
                        indexList.Add($"{ix + x}{iy + y}");
                        filledRooms++;
                    }
                }
            }
        }
        /// <summary>
        /// Chances of the different rooms
        /// Returns which room it should be
        /// </summary>
        /// <returns></returns>
        private RoomType Chance()
        {
            switch ((sbyte)rnd.Next(0, 101))
            {
                case sbyte n when (n > -1 && n < 15): //15% chance for loot
                    return RoomType.Loot;
                case sbyte n when (n > 75 && n < 101)://25% chance for hard room
                    return RoomType.Hard;
                default:                       //60% chance for normal room
                    return RoomType.Normal;

            }

        }
        /// <summary>
        /// Chances for a room to have the style it has.
        /// Aka what walls, floors and decorations should be in.
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CreateStyle(byte t, byte x, byte y)
        {
            if (t == 0) t += 2; //failsafe
            if (t > 2) roomStyle[x, y] = (byte)rnd.Next(0 + (t - 1), 10); //The harder or rarer the room, the prettier it should be. Higher values returned.
            else roomStyle[x, y] = (byte)rnd.Next(0, 10 - (t + 1)); //The easier the room, the lower the value returned.

            roomStyle[x,y] = (byte)rnd.Next(0, 3);
        }

        /// <summary>
        /// Console.WriteLine debug method
        /// To view: Right click EksamensProjekt2021.crsproj -> properties.
        /// Outputtype: Console Application.
        /// </summary>
        /// <param name="failSafe"></param>
        /// <param name="reruns"></param>
        public void Debug(byte failSafe, byte reruns)
        {
            //To view: Right click EksamensProjekt2021.crsproj -> properties.
            //Outputtype: Console Application.
            if (GameWorld.HCDebug == true)
            {
                Console.Clear();
                for (int y = 0; y < roomLayout.GetLength(0); y++)
                {
                    for (int x = 0; x < roomLayout.GetLength(1); x++)
                    {
                        if (roomLayout[x, y] == 0) Console.ForegroundColor = ConsoleColor.White;
                        else Console.ForegroundColor = ConsoleColor.Green;
                        if (revealedRoom[x, y] == true) Console.ForegroundColor = ConsoleColor.Magenta;
                        if (x == playerInRoom[0] && y == playerInRoom[1]) Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(roomLayout[x, y] + " ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    if (failSafe >= 100) Console.WriteLine("  BROKE");
                    else Console.WriteLine("");
                }
                Console.WriteLine($"\nReruns: {reruns}");
                mapReruns = 0;
            }
        }
        /// <summary>
        /// Resets everything to do with the map
        /// </summary>
        private void Reset()
        {
            for (int x = 0; x < roomLayout.GetLength(0); x++) // Reset map. Bruges til at lave nye levels.
            {
                for (int y = 0; y < roomLayout.GetLength(1); y++)
                {
                    roomLayout[x, y] = 0;
                    roomStyle[x, y] = 0;
                    revealedRoom[x, y] = false;
                }
            }
            indexList.Clear();
            filledRooms = 0;
            failSafe = 0; //If the code messes up, use this to escape
            byte rndX = (byte)rnd.Next(1, 4);
            byte rndY = (byte)rnd.Next(1, 4);
            roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
            CreateStyle(1, rndX, rndY); //Create style for inital room
            index = new int[2] { rndX, rndY, }; //Sets index to spawn room.
            playerInRoom[0] = rndX; //Set new player coords.
            playerInRoom[1] = rndY;
        }

        /// <summary>
        /// Fill in walls and object using this method.
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        public void InitialiseRoom(byte indexX, byte indexY)
        {

            switch (roomStyle[indexX, indexY])

            {
                default:
                    break;
            }
        }
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 3; i++)
            {
                floor[i] = content.Load<Texture2D>($"Floor{i}");
            }
            wall = content.Load<Texture2D>("Wall1");
        }
        /// <summary>
        /// Draw the rooms walls & floor using this method
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        public void DrawRoom(SpriteBatch spriteBatch)
        {
            switch (roomStyle[playerInRoom[0], playerInRoom[1]])
            {
                case 2:
                    spriteBatch.Draw(floor[2], Vector2.Zero, Color.White);
                    spriteBatch.Draw(wall, Vector2.Zero, Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(floor[1], Vector2.Zero, Color.White);
                    spriteBatch.Draw(wall, Vector2.Zero, Color.White);
                    break;
                default:
                    spriteBatch.Draw(floor[0], Vector2.Zero, Color.White);
                    spriteBatch.Draw(wall, Vector2.Zero, Color.White);
                    break;
            }
        }
        public void RevealRooms()
        {
            byte x = playerInRoom[0];
            byte y = playerInRoom[1];
            revealedRoom[x, y] = true;
            for (int i = -1; i < 2; i += 2)
            {
                if (x + i > -1 && x + i < 5) //Out of bounds failsafe
                {
                    if (roomLayout[x + i, y] >= 1)
                    {
                        revealedRoom[x + i, y] = true; //Reveal rooms left and right
                        Console.Write($"x{i} true");
                    }
                }
                if (y + i > -1 && y + i < 5)
                {
                    if (roomLayout[x, y + i] >= 1)
                    {
                        revealedRoom[x, y + i] = true; //Reveal room up and down
                        Console.Write($"y{i} true");
                    }
                }
            }
        }
    }
}
