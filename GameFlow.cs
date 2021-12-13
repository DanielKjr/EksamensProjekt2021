using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class GameFlow
    {

        public void EnemySpawner()
        {
                addEnemy();
                addEnemy();
                addEnemy();
                addEnemy();
        }
        public void addEnemy()
        {
            Random rnd = new Random();

            var EXPos = rnd.Next(0, (int)GameWorld.screenSize.X);
            var EYPOS = rnd.Next(0, (int)GameWorld.screenSize.Y);
            GameWorld.Instantiate(new Enemy(new Vector2(EXPos, EYPOS), new Tomahawk()));
        }

    }
}
