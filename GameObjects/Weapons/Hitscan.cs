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

        protected Texture2D projectile;


        public Hitscan()
        {

            
            

        }

        public Hitscan(Texture2D sprite, Vector2 Position, Vector2 target)
        {
            this.sprite = sprite;
            position = Position;
            this.target = target;
            this.origin = Vector2.Zero;
            moveSpeed = 1900;
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

            GameWorld.Instantiate(new Hitscan(sprite, Position, target));

        }

        public void HitScanShoot(GameTime gameTime, Vector2 target)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

        }

        public override void Update(GameTime gameTime)
        {

            HitScanShoot(gameTime, target);
        }
        public override void LoadContent(ContentManager content)
        {
             sprite = content.Load<Texture2D>("CollisionTexture ");

           
        }
    }
}
