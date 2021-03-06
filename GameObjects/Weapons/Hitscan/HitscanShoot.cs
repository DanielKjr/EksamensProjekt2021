using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class HitscanShoot : Hitscan
    {


        public HitscanShoot(Vector2 Position, Vector2 target, byte damage)
        {
            this.damage = damage;
            this.Position = Position;
            this.target = target;
            origin = Vector2.Zero;
            moveSpeed = 1700;
        }

        /// <summary>
        /// Similar to ProjectileShoot, just faster. Takes the direction from mouse position 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="target"></param>
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

        public override void Update(GameTime gameTime)
        {

            HitScanShooting(gameTime, target);

           
        }

        public override void LoadContent(ContentManager content)
        {
            
            sprite = content.Load<Texture2D>("CollisionTexture ");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {              
                other.Health -= damage;
                GameWorld.Despawn(this);              
            }
        }

    }
}
