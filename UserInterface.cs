using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EksamensProjekt2021
{
    public class UserInterface
    {
        private Weapon weapon;
        private Texture2D wSprite;
        private Texture2D UIBox1280;
        private Texture2D UIBox680;
        private Texture2D UIBox680360;
        private Vector2 boxPosition = new Vector2((GameWorld.screenSize.X / 4), GameWorld.screenSize.Y);
        private int roomsCleared = 4;

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

            wSprite = GameWorld.player.currentWeapon.Sprite;
            UIBox1280 = content.Load<Texture2D>("UIBox1280");
            UIBox680 = content.Load<Texture2D>("UIBox680");
            UIBox680360 = content.Load<Texture2D>("UIBox680360");
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kState = Keyboard.GetState();
            weapon = GameWorld.player.currentWeapon;
            if (kState.IsKeyDown(Keys.Tab) && boxPosition.Y >= 370)
            {
                boxPosition.Y -= 650 * dt;
            }
            if (kState.IsKeyUp(Keys.Tab) && boxPosition.Y <= GameWorld.screenSize.Y)
            {
                boxPosition.Y += 650 * dt;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(UIBox680360, new Vector2(boxPosition.X, boxPosition.Y), Color.White);
            if (wSprite != null)
            {
                spriteBatch.Draw(wSprite, new Rectangle((int)boxPosition.X + 70, (int)boxPosition.Y + 36, 65, 40), Color.White);
                spriteBatch.DrawString(GameWorld.HUDFont, $"{GameWorld.player.currentWeapon.Name}", new Vector2(boxPosition.X + 70, boxPosition.Y + 8), Color.White);
            }
            spriteBatch.DrawString(GameWorld.HUDFont, $"Rooms Cleared:{roomsCleared}", new Vector2(boxPosition.X + 375, boxPosition.Y + 5), Color.White);
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
