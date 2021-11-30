using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class Projectile : Throwable
    {
        public Projectile(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            Position = position;
            moveSpeed = 500;
        }


        /// <summary>
        /// The animation/movement of the projectile from the enemy position to the PlayerPosition.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="playerPosition"></param>
        public void ProjectileShoot(GameTime gameTime, Vector2 playerPosition)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = PlayerPosition - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;
            
            if (Vector2.Distance(Position, PlayerPosition) < 10)
            {
                GameWorld.Despawn(this);
                //TODO add damage to player
            }
            
            
        }

        public override void Update(GameTime gameTime)
        {
            ProjectileShoot(gameTime, PlayerPosition);

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
