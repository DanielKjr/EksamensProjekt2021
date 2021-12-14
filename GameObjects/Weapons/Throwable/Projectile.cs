using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
     class Projectile : Throwable
    {
        
        

        public Projectile(Texture2D sprite, Vector2 position, Vector2 target, byte damage, float throwRotationSpeed)
        {
            this.damage = damage;
            this.sprite = sprite;
            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;
            this.throwRotationSpeed = throwRotationSpeed;
            throwRotation -= throwRotationSpeed;
            moveSpeed = 400;

            
        }

        public Projectile(Texture2D sprite, Vector2 position, Vector2 target, byte damage, float throwRotationSpeed, bool canHurtPlayer)
        {
            this.damage = damage;
            this.sprite = sprite;
            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;
            this.throwRotationSpeed = throwRotationSpeed;
            throwRotation -= throwRotationSpeed;
            moveSpeed = 400;
            this.canHurtPlayer = canHurtPlayer;


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
            throwRotation += throwRotationSpeed;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy && !canHurtPlayer)
            {
                other.Health -= damage;
                
                GameWorld.Despawn(this);
               
            }

            if (other is Player && canHurtPlayer)
            {
                GameWorld.player.Damage(damage);
                GameWorld.Despawn(this);
            }

            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(sprite, position, null, Color.White, throwRotation, origin, 1, weaponMirror, 0);

        }



    }
}
