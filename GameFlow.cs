using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class GameFlow
    {
        /// <summary>
        /// adds enemies
        /// </summary>
        public void EnemySpawner()
        {
                addEnemy();
                addEnemy();
                addEnemy();
                addEnemy();
        }

        /// <summary>
        /// adds enemies with random position and weapon
        /// </summary>
        public void addEnemy()
        {
            Random rnd = new Random();

            var EXPos = rnd.Next(0, (int)GameWorld.screenSize.X); //sets a random X value
            var EYPOS = rnd.Next(0, (int)GameWorld.screenSize.Y); //sets a random Y value

            int weapon = rnd.Next(0, 1);

            if (weapon == 0)                                      //50/50 chance that enemy has a tomahowk or molotov
            {
                GameWorld.Instantiate(new Enemy(new Vector2(EXPos, EYPOS), new Tomahawk())); 
            }
            else
            {
                //GameWorld.Instantiate(new Enemy(new Vector2(EXPos, EYPOS), new Molotov()));
            }

        }

    }
}
