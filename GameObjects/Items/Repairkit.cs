using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace EksamensProjekt2021
{
    class Repairkit : Item
    {
        private int armorPlus = 15;

        public Repairkit(Vector2 Position, byte x, byte y)
        {
            position = Position;
            xByte = x;
            yByte = y;
        }

        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("repairkit");
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
                if (other.Armor != 50)
                {
                    player.Armor += armorPlus;
                    GameWorld.Despawn(this);
                }

            }
        }



    }


}


