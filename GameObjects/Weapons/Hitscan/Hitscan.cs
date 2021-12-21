using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Hitscan : Weapon
    {

     
        public Hitscan()
        {

        }

     

        /// <summary>
        /// This version simply takes position of the weapon and a target, it all uses the same projectile.
        /// </summary>
        /// <param name="target"></param>
        public override void ShootWeapon(Vector2 target)
        {
            GameWorld.Instantiate(new HitscanShoot(Position, target, damage));

        }


        public override void Update(GameTime gameTime)
        {


        }
        public override void LoadContent(ContentManager content)
        {
            



        }



    }
}
