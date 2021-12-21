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
        public static byte[,] roomStyle = new byte[5, 5]; //0-3
        public static bool[,] revealedRoom = new bool[5, 5];
        public static sbyte[] playerInRoom = new sbyte[2]; //See which room the player is in. (X, Y)

        public static int roomsCleared;
        public static int levelsCleared;

        public static byte mapReruns = 0; //See how many times the RoomsGenerator needed to run
        private Texture2D[] floor = new Texture2D[4];
        private Texture2D wall;

        private byte filledRooms = 0;
        private byte failSafe = 0;
        private int[] index = new int[2];
        private Random rnd = new Random();

        /// <summary>
        /// Checks if the player has cleared a room
        /// </summary>
        public void Update()
        {
            if (GameWorld.EnemyCount <= 0) //Checks if the player has cleared a room
            {
                if (roomLayout[playerInRoom[0], playerInRoom[1]] == 5) //if player has cleared level.
                {
                    roomsCleared++;
                    levelsCleared++;
                    foreach (var item in GameWorld.gameObjects) //Deletes all leftover items
                    {
                        if (item is Item)
                        {
                            GameWorld.Despawn(item);
                        }
                    }
                    CreateMap(9);
                }
                else if (roomLayout[playerInRoom[0], playerInRoom[1]] >= 2) //Clear room in level
                {
                    roomLayout[playerInRoom[0], playerInRoom[1]] = (byte)RoomType.Cleared;
                    roomsCleared++;
                }
                GameWorld.EnemyCount = 0; //Failsafe hvis nu at en enemy ikke plusser sin værdi til enemyCount
            }
        }
        /// <summary>
        /// Generates the map based on amount of rooms desired.
        /// Will auto retry if it fails (Never fails tho)
        /// </summary>
        /// <param name="amountOfRooms"></param>
        public void CreateMap(byte amountOfRooms)
        {
            Reset();
            mapReruns = 0;

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
                        CreateStyle(ix + x, iy + y);
                        indexList.Add($"{ix + x}{iy + y}"); //Add to index list for future use
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
        private void CreateStyle(int x, int y)
        {
            roomStyle[x, y] = (byte)rnd.Next(0, 3);
            if (roomLayout[x, y] == 1) roomStyle[x, y] = 3;
            //I første version tænkte vi at denne skulle være større, men det gav problemer overalt hvis at vi brugte 0-9 i stedet for 0-2
            //Ved brug af 0-9 kunne vi opbevarer om der skulle være ekstra vægge inde i rummet. Aka: Flere detaljer kunne gemmes
        }
        /// <summary>
        /// Console.WriteLine debug method
        /// To view: Right click EksamensProjekt2021.crsproj -> properties.
        /// Outputtype: Console Application.
        /// Also! Set RoomManagerDebug in GameWorld to true.
        /// </summary>
        /// <param name="failSafe"></param>
        /// <param name="reruns"></param>
        public void Debug(byte failSafe, byte reruns)
        {
            //To view: Right click EksamensProjekt2021.crsproj -> properties.
            //Outputtype: Console Application.
            //Vil crashe hvis du tænder uden at være konsol applikation.
            if (GameWorld.RoomManagerDebug == true)
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
            mapReruns++;
            indexList.Clear();
            filledRooms = 0;
            failSafe = 0; //If the code messes up, use this to escape
            byte rndX = (byte)rnd.Next(1, 4);
            byte rndY = (byte)rnd.Next(1, 4);
            roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
            CreateStyle(rndX, rndY); //Create style for inital room
            index = new int[2] { rndX, rndY, }; //Sets index to spawn room.
            playerInRoom[0] = (sbyte)rndX; //Set new player coords.
            playerInRoom[1] = (sbyte)rndY;
        }
        /// <summary>
        /// Loads Wall & Door sprites
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 4; i++)
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
            //Var Switch Case før, nu rettet til at være mere simpel.
            spriteBatch.Draw(floor[roomStyle[playerInRoom[0], playerInRoom[1]]], Vector2.Zero, Color.White);
            spriteBatch.Draw(wall, Vector2.Zero, Color.White);
        }
        /// <summary>
        /// Checks what rooms are visible to the player.
        /// Needed for displayMap() in UserInterface
        /// </summary>
        public void RevealRooms()
        {
            sbyte x = playerInRoom[0];
            sbyte y = playerInRoom[1];
            revealedRoom[x, y] = true;
            for (int i = -1; i < 2; i += 2)
            {
                if (x + i > -1 && x + i < 5) //Out of bounds failsafe
                {
                    if (roomLayout[x + i, y] >= 1)
                    {
                        revealedRoom[x + i, y] = true; //Reveal rooms left and right
                    }
                }
                if (y + i > -1 && y + i < 5)//Out of bounds failsafe
                {
                    if (roomLayout[x, y + i] >= 1)
                    {
                        revealedRoom[x, y + i] = true; //Reveal room up and down
                    }
                }
            }
        }
        /// <summary>
        /// Called when the player changes rooms. Called in Door OnCollision.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangeRoom(sbyte x, sbyte y)
        {
            //Change player pos to match entering new room from that door
            if (x == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X - 146, GameWorld.screenSize.Y / 2);
            if (x == 1) GameWorld.player.Position = new Vector2(146, GameWorld.screenSize.Y / 2);
            if (y == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y - 144);
            if (y == 1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, 144);

            playerInRoom[0] += x; //Sets player room pos to new room
            playerInRoom[1] += y;

            Debug(0, 0);
            RevealRooms();

            switch (roomLayout[playerInRoom[0], playerInRoom[1]])
            {
                case 2:
                    GameWorld.gameFlow.EnemySpawner(); //'Normal' Room. Spawn 1 enemy batch
                    break;
                case 3:
                    GameWorld.gameFlow.EnemySpawner(); //'Hard' Room. Spawn 2 batches of enemies.
                    GameWorld.gameFlow.EnemySpawner();
                    break;
                case 4:
                    GameWorld.gameFlow.LootSpawner(); //'Loot' Room. Spawn loot.
                    break;
                case 5:
                    GameWorld.Instantiate(new Biden()); //Make a new boss.
                    GameWorld.bossSpawned = true; //To draw boss healthbar
                    break;
            }
        }
    }
}
