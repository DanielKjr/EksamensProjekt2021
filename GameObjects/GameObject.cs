using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public abstract class GameObject : GameWorld
    {
        protected Vector2 position;
        protected Texture2D sprite;
        protected Texture2D[] sprites;

        protected int fps;
        protected float moveSpeed;
        protected int health;
        protected int armor;

        public abstract void Shoot(Weapon weapon);

        //get rectangle
        public Rectangle Collision
        {
            get
            {
                return new Rectangle(
                    (int)(position.X),
                    (int)(position.Y),
                    sprite.Width,
                    sprite.Height
                    );
            }
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public abstract void Update(GameTime gameTime);

        //måske abstract? ikke sikker ud fra uml
        protected void Animate(GameTime gameTIme)
        {

        }

        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (Collision.Intersects(other.Collision))
            {
                OnCollision(other);
            }

        }

    }
}
