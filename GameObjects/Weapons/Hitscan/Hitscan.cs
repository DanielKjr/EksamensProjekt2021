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

        public override void ShootWeapon(Vector2 target)
        {

            GameWorld.Instantiate(new HitscanShoot(sprite, Position, target));

        }

        

        public override void Update(GameTime gameTime)
        {
            
            
        }
        public override void LoadContent(ContentManager content)
        {
            
             //sprite = content.Load<Texture2D>("CollisionTexture ");

           
        }
    }
}
