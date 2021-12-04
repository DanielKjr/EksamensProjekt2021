using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class Projectile : Throwable
    {



        public Projectile(Texture2D sprite, Vector2 position, Vector2 target)
        {
            this.sprite = sprite;
            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;

            moveSpeed = 400;
        }


        /// <summary>
        /// The animation/movement of the projectile from the position of the shooter to the target position.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="playerPosition"></param>
        public void ProjectileShoot(GameTime gameTime, Vector2 target)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;




        }



        public override void LoadContent(ContentManager content)
        {



        }


        public override void Update(GameTime gameTime)
        {

            ProjectileShoot(gameTime, target);

            if (Vector2.Distance(Position, target) < 10)
            {
                GameWorld.Despawn(this);
                //TODO add damage to player
            }

        }

        public override void OnCollision(GameObject other)
        {//virker ikke lige nu da han ikke har nogen hitbox 
            if (other is Player)
            {
                GameWorld.Despawn(this);
            }
        }




    }
}
