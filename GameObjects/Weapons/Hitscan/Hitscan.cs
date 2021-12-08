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

        //if Hitscan has any unique attributes this is where they should be
       

        public static void Shoot()
        {
            MouseState mState = Mouse.GetState();
            mState.Position.ToVector2();
            Vector2 mPos = mState.Position.ToVector2();

            Rectangle rectangle;
            rectangle.X = (int)mPos.X;
            rectangle.Y = (int)mPos.Y;
            rectangle.Height = 1;
            rectangle.Width = 1;
        }

        /// <summary>
        /// This version simply takes position of the weapon and a target, it all uses the same projectile.
        /// </summary>
        /// <param name="target"></param>
        public override void ShootWeapon(Vector2 target)
        {

            GameWorld.Instantiate(new HitscanShoot(Position, target));

           

        }

       

        public override void Update(GameTime gameTime)
        {
            
            
        }
        public override void LoadContent(ContentManager content)
        {
            //skal være her men tror ikke noget skal loades her
            

           
        }
    }
}
