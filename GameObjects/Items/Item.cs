using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Item : GameObject
    {
        public List<Item> items = new List<Item>();

        protected int nextWeapon;
        protected bool show;
        protected byte xByte;
        protected byte yByte;
        public int NextWeapon { get => nextWeapon; set => nextWeapon = value; }



      
        public override void LoadContent(ContentManager content)
        {
           
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
             if (show) spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
