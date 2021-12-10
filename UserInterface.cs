using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    public class UserInterface
    {
        Color color;
        byte dist = 10;
        int offsetX;
        int offsetY;
        private Texture2D sprite;
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("CollisionTexture ");
            offsetX = (int)GameWorld.screenSize.X - RoomManager.roomLayout.GetLength(0) * sprite.Width - RoomManager.roomLayout.GetLength(0) * dist;
            offsetY = (int)GameWorld.screenSize.Y - RoomManager.roomLayout.GetLength(1) * sprite.Width - RoomManager.roomLayout.GetLength(0) * dist;
        }
        public void mapDisplay(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < RoomManager.roomLayout.GetLength(1); y++)
            {
                for (int x = 0; x < RoomManager.roomLayout.GetLength(0); x++)
                {
                    if (RoomManager.playerInRoom[0] == x && RoomManager.playerInRoom[1] == y) color = Color.Green;
                    else if (RoomManager.roomLayout[x, y] == 5) color = Color.Black;
                    else if (RoomManager.roomLayout[x, y] == 4) color = Color.Cyan;
                    else if (RoomManager.roomLayout[x, y] >= 2) color = Color.Red;
                    else color = Color.White;

                    if (RoomManager.revealedRoom[x, y] || RoomManager.roomLayout[x, y] == 5)
                    {
                        spriteBatch.Draw(sprite, new Vector2(offsetX + (sprite.Width * x + dist * x), offsetY + (sprite.Height * y + dist * y)), color);
                    }
                }
            }
        }
    }
}
