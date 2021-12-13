using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
     class Throwable : Weapon
    {
        protected float throwRotation = 0f;
        protected float throwRotationSpeed;

        public Throwable()
        {
            
            
        }

        /// <summary>
        /// This version of the ShootWeapon function will instantiate a projectile from the current position towards the target.
        /// The prerequisites for firing should be in the class that calls the function.
        /// </summary>
        public override void ShootWeapon(Vector2 target)
        {

            //hvis vi kun har den her, så står våbnet kun for at skyde, hvilket er en god ting, tror jeg
            GameWorld.Instantiate(new Projectile(sprite, Position, target, damage, throwRotationSpeed));


           
        }

        public void HitScanShooting(GameTime gameTime, Vector2 target)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

            if (Vector2.Distance(Position, target) < 20)
            {
                GameWorld.Despawn(this);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            
           
           
        }
        
    }
}
