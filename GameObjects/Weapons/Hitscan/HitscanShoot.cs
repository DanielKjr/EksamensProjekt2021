using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class HitscanShoot : Hitscan
    {
        public HitscanShoot(Texture2D sprite, Vector2 Position, Vector2 target) : base()
        {
           this.sprite = sprite;
            position = Position;
            this.target = target;
            this.origin = Vector2.Zero;
            moveSpeed = 1900;
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
           // sprite = content.Load<Texture2D>("CollisionTexture ");
        }
    }
}
