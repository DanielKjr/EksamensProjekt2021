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
           

        }


      
        /// <summary>
        /// This version of the ShootWeapon function will instantiate a projectile if the User and target is within range
        /// </summary>
        public override void ShootWeapon()
        {
            
            if (Vector2.Distance(Position, target) < 500)
            {
               
               GameWorld.Instantiate(new Projectile(sprite, Position, target));


            }
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
