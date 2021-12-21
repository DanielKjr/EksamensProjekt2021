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

        public Medkit(Vector2 Position, byte x, byte y)
        {
            position = Position;
            xByte = x;
            yByte = y;
        }




        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("medkit");
        }

        public override void Update(GameTime gameTime)
        {
            if (RoomManager.playerInRoom[0] == xByte && RoomManager.playerInRoom[1] == yByte)
            {
                show = true;
            }
            else show = false;
        }


        public override void OnCollision(GameObject other)
        {
            if (other is Player player && show)
            {
                if (other.Health != 100)
                {
                    player.Health += healthplus;
                    GameWorld.Despawn(this);
                }
                
            }
        }
    }


}
