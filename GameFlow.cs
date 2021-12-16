using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class GameFlow
    {


        protected int nextWeapon;

        public int NextWeapon { get => nextWeapon; }


        private Random rnd = new Random();

        /// <summary>
        /// adds enemies
        /// </summary>
        public void EnemySpawner()
        {
            for (int i = 0; i < rnd.Next(2,RoomManager.levelsCleared+3); i++)
            {
                AddEnemy();
            }
        }
        public void LootSpawner()
        {
            for (int i = 0; i < rnd.Next(1,3); i++) //Spawns 1-2 medkits
            {
                GameWorld.Instantiate(new Medkit(new Vector2(rnd.Next(0, (int)GameWorld.screenSize.X), rnd.Next(0, (int)GameWorld.screenSize.Y)),
                    (byte)RoomManager.playerInRoom[0], (byte)RoomManager.playerInRoom[1]));
            }

            
                SpawnNewWeapon();
            
          
           
            //Add weapon spawner here.

           

        }

        public void SpawnNewWeapon()
        {
            nextWeapon = rnd.Next(0,101);
            

            if (nextWeapon >= 20)
            {
                GameWorld.Instantiate(new WeaponPickup(new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y /2)));
            }
           
        }


        /// <summary>
        /// adds enemies with random position and weapon
        /// </summary>
        public void AddEnemy()
        {
            


            var EXPos = rnd.Next(0, (int)GameWorld.screenSize.X); //sets a random X value
            var EYPOS = rnd.Next(0, (int)GameWorld.screenSize.Y); //sets a random Y value

            int weapon = rnd.Next(0, 1);

            if (weapon == 0)                                      //50/50 chance that enemy has a tomahowk or molotov
            {
                GameWorld.Instantiate(new Enemy(new Vector2(EXPos, EYPOS), new Tomahawk(true))); 
            }
            else
            {
                //GameWorld.Instantiate(new Enemy(new Vector2(EXPos, EYPOS), new Molotov()));
            }


        }

    }
}
