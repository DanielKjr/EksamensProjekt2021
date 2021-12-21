using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public abstract class GameObject
    {
        protected Vector2 playerPosition;
        protected Vector2 position;
        protected Vector2 target;
        protected Vector2 origin;
        protected Texture2D sprite;
        protected Texture2D[] sprites;

        protected int fps;
        protected float moveSpeed;
        protected int health;
        protected int armor;
        protected bool isAlive;


        
        public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
        public bool IsAlive { get => isAlive; }
        public int Health { get => health; set => health = value; }
        public Vector2 Position { get => position; set => position = value; }
        public int Armor { get => armor; set => armor = value; }

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

       
     


        public abstract void LoadContent(ContentManager content);
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {         
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Decides what happens when two oblects' hitboxes collide
        /// </summary>
        /// <param name="other"></param>
        public abstract void OnCollision(GameObject other);

        /// <summary>
        /// Checks whether or not two Collision rectangles interesects
        /// </summary>
        /// <param name="other"></param>
        public void CheckCollision(GameObject other)
        {
            if (Collision.Intersects(other.Collision))
            {
                OnCollision(other);
            }

        }

    }
}
