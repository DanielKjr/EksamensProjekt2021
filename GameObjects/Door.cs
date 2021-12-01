using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class Door //Conjoined with RoomManager
    {
        Vector2 pos;
        Texture2D sprite;
        string placement;
        public Door(string placement)
        {
            this.placement = placement;
        }
        private void LoadContent(ContentManager content)
        {
            switch (placement)
            {
                case "up":
                    //sprite = content.Load<Texture2D>("DoorHorizontal");
                    pos = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, 0);
                    break;
                case "down":
                    //sprite = content.Load<Texture2D>("DoorHorizontal");
                    pos = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, GameWorld.screenSize.Y - sprite.Height);
                    break;
                case "left":
                    //sprite = content.Load<Texture2D>("DoorVertical");
                    pos = new Vector2(0, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    break;
                case "right":
                    //sprite = content.Load<Texture2D>("DoorVertical");
                    pos = new Vector2(GameWorld.screenSize.X - sprite.Width, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    break;
            }
        }
    }
}
