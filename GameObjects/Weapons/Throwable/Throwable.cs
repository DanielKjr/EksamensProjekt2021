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

        public override Rectangle Collision
        {
            get {
                return new Rectangle(
              (int)(Position.X - 20 ),
              (int)(Position.Y - 20),
              sprite.Height,
              sprite.Width);
            }
        }

        /// <summary>
        /// This version of the ShootWeapon function will instantiate a projectile from the current position towards the target.
        /// The prerequisites for firing should be in the class that calls the function.
        /// </summary>
        public override void ShootWeapon(Vector2 target)
        {

            if (!canHurtPlayer)
            {
                GameWorld.Instantiate(new Projectile(sprite, Position, target, damage, throwRotationSpeed, origin));
            }
            else
            {
                GameWorld.Instantiate(new Projectile(sprite, Position, target, damage, throwRotationSpeed, true, origin));
            }
           
           




        }


        public override void LoadContent(ContentManager content)
        {



        }

    }
}
