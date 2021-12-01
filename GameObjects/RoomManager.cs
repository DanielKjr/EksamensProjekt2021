using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class RoomManager
    {
        public static byte[,] roomLayout = new byte[5, 5];
        public static byte mapReruns = 0; //See how many times the RoomsGenerator needed to run

        /// <summary>
        /// Generates the map based on amount of rooms desired.
        /// Will auto retry if it fails (random WILL fail)
        /// </summary>
        /// <param name="amountOfRooms"></param>
        public void RoomsGenerator(byte amountOfRooms)
        {
            RoomsReset();
            Random rnd = new Random();
            byte createdRooms = 0;
            byte filledRooms = 0;
            byte failSafe = 0; //If the code messes up, use this to escape

            byte rndX = (byte)rnd.Next(1, 4);
            byte rndY = (byte)rnd.Next(1, 4);
            roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
            int[] index = new int[2] { rndX, rndY, }; //Sets index to spawn room.


            while (filledRooms < amountOfRooms)
            {
                for (int x = 0; x < roomLayout.GetLength(0); x++)//Find next index point
                {
                    for (int y = 0; y < roomLayout.GetLength(1); y++)
                    {
                        if (roomLayout[x, y] == 255)
                        {
                            index[0] = x;
                            index[1] = y;
                            roomLayout[index[0], index[1]] = RoomChance(); //See what this room should become
                            filledRooms++;
                            break;
                        }
                    }
                }



                if (createdRooms < amountOfRooms) //Only create a new room, if there is a need for it.
                {
                    for (int x = -1; x <= 1; x += 2) //Check left and right
                    {
                        if (index[0] + x == -1 || index[0] + x == 5) break; //Check if outside
                        if (roomLayout[index[0] + x, index[1]] == 0) //Check if empty
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    roomLayout[index[0] + x, index[1]] = 255; //Fill temporary value into room
                                    createdRooms++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    for (int y = -1; y <= 1; y += 2) //Check up and down
                    {
                        if (index[1] + y == -1 || index[1] + y == 5) break; //Check if outside
                        if (roomLayout[index[0], index[1] + y] == 0) //Check if clear
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    roomLayout[index[0], index[1] + y] = 255; //Fill temporary value into room
                                    createdRooms++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                failSafe++;
                if (failSafe >= 100) //If the map failed, run again
                {
                    createdRooms = 0;
                    filledRooms = 0;
                    failSafe = 0;
                    RoomsReset();
                    rndX = (byte)rnd.Next(1, 4);
                    rndY = (byte)rnd.Next(1, 4);
                    roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
                    index = new int[2] { rndX, rndY, }; //Sets index to spawn room.
                    mapReruns++;
                }
            }
            roomLayout[index[0], index[1]] = 5; //Set last created room to be boss room


            RoomDebug(failSafe, mapReruns);
            //To view: Right click EksamensProjekt2021.crsproj -> properties.
            //Outputtype: Console Application.
        }
        /// <summary>
        /// Chances of the different rooms
        /// Returns which room it should be
        /// </summary>
        /// <returns></returns>
        public byte RoomChance()
        {
            Random rnd = new Random();
            switch ((int)rnd.Next(0, 101))
            {
                case int n when (n > -1 && n < 15): //15% chance for loot
                    return 4;
                case int n when (n > 75 && n < 101)://25% chance for hard room
                    return 3;
                default:                       //60% chance for normal room
                    return 2;
            }
        }
        /// <summary>
        /// Console.WriteLine debug method
        /// To view: Right click EksamensProjekt2021.crsproj -> properties.
        /// Outputtype: Console Application.
        /// </summary>
        /// <param name="failSafe"></param>
        /// <param name="reruns"></param>
        private void RoomDebug(byte failSafe, byte reruns)
        {
            //To view: Right click EksamensProjekt2021.crsproj -> properties.
            //Outputtype: Console Application.
            Console.Clear();
            for (int y = 0; y < roomLayout.GetLength(0); y++)
            {
                for (int x = 0; x < roomLayout.GetLength(1); x++)
                {
                    if (roomLayout[x, y] == 0) Console.ForegroundColor = ConsoleColor.White;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(roomLayout[x, y] + " ");
                }
                if (failSafe >= 100) Console.WriteLine("  BROKE");
                else Console.WriteLine("");
            }
            Console.WriteLine($"\nReruns: {reruns}");
            mapReruns = 0;
        }
        /// <summary>
        /// Clears Map[] array.
        /// </summary>
        public void RoomsReset()
        {
            for (int x = 0; x < roomLayout.GetLength(0); x++) // Reset map. Bruges til at lave nye levels.
            {
                for (int y = 0; y < roomLayout.GetLength(1); y++)
                {
                    roomLayout[x, y] = 0;
                }
            }
        }
    }
}
