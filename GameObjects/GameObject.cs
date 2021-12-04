using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public abstract class GameObject
    {
        protected Vector2 position;
       
        protected Vector2 origin;
        protected Texture2D sprite;
        protected Texture2D[] sprites;

        protected int fps;
        protected float moveSpeed;
        protected int health;
        protected int armor;

        public Vector2 playerPosition;
        public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
        // public abstract void Shoot();

        //public abstract void Shoot(Weapon weapon);
        //den skal bruges senere men for at teste uden vi har våben lavede jeg en uden constructors


        //get rectangle
        public virtual Rectangle Collision
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

       
        public Vector2 Position { get => position; set => position = value; }
        protected int Health { get => health; set => health = value; }

        public abstract void LoadContent(ContentManager content);
        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
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
