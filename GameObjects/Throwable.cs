using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class Throwable : Weapon
    {


        public Throwable()
        {
            range = 500;
            fireRate = 2;
            
        }



        /// <summary>
        /// This version of the ShootWeapon function will instantiate a projectile from the current position towards the target.
        /// The prerequisites for firing should be in the class that calls the function.
        /// </summary>
        public override void ShootWeapon(Vector2 target)
        {

            //hvis vi kun har den her, så står våbnet kun for at skyde, hvilket er en god ting, tror jeg
            GameWorld.Instantiate(new Projectile(sprite, Position, target));


            /*
            if (Vector2.Distance(Position, target) < 500)
            {
               
               GameWorld.Instantiate(new Projectile(sprite, Position, target));


            }
            */
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enemy2");
           
           
        }
    }
}
