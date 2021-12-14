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



        /// <summary>
        /// This version of the ShootWeapon function will instantiate a projectile from the current position towards the target.
        /// The prerequisites for firing should be in the class that calls the function.
        /// </summary>
        public override void ShootWeapon(Vector2 target)
        {

            if (!canHurtPlayer)
            {
                GameWorld.Instantiate(new Projectile(sprite, Position, target, damage, throwRotationSpeed));
            }
            else
            {
                GameWorld.Instantiate(new Projectile(sprite, Position, target, damage, throwRotationSpeed, true));
            }
           
           




        }


        public override void LoadContent(ContentManager content)
        {



        }

    }
}
