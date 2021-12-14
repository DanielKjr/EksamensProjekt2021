using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Biden : Enemy
    {
        public SpriteAnimation anim;
        private Texture2D biden;
        private Vector2 bidenDestination = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y / 2);
        private Random rand = new Random();
        public Biden()
        {
            Position = new Vector2(800, 500);
            moveSpeed = 500;
            health = 200;

        }

        public override void LoadContent(ContentManager content)
        {
            biden = content.Load<Texture2D>("biden");
            anim = new SpriteAnimation(biden, 5, 9);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

            }
        }

        public override void Movement(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = bidenDestination - new Vector2(Position.X - 50, Position.Y - 70);
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;
            if (Vector2.Distance(position, bidenDestination) < 200)
            {
                bidenDestination = new Vector2(rand.Next(50, (int)GameWorld.screenSize.X - 50), rand.Next(50, (int)GameWorld.screenSize.Y - 50));
            }


        }
        public override void Update(GameTime gameTime)
        {
            anim.Texture = biden;
            anim.Update(gameTime);
            anim.Position = new Vector2(Position.X - 50, Position.Y - 70);
            Movement(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            anim.Draw(spriteBatch);
        }
        public override Rectangle Collision
        {
            get
            {


                return new Rectangle(
                               (int)(anim.Position.X),
                               (int)(anim.Position.Y),
                               100,
                               140
                               );

            }
        }
    }
}
