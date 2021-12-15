using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Medkit : Item
    {
        private int healthplus = 20;
        
        public int Healthplus { get => healthplus; set => healthplus = value; }



        //public Item (string medkit)
        //{
        //    healthplus = 20;
        //    type = medkit;
        //    Position = new Vector2(400, 400);
        //}

        public Medkit(Vector2 Position, byte x, byte y)
        {
            this.position = Position;
            Healthplus = healthplus;
            xByte = x;
            yByte = y;
        }




        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("medkit");
        }

        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (show) spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player player && show)
            {
                if (other.Health != 100)
                {
                    player.MedkitHeal(Healthplus);
                    GameWorld.Despawn(this);
                }
                
            }
        }
    }


}
