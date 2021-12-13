﻿using System;
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
        private Texture2D UIBox276;
        private Texture2D trumpGraph;
        private Texture2D trumpSad;
        private Vector2 boxPosition;

        private Vector2 trumpVector = new Vector2((GameWorld.screenSize.X / 2 + 30), GameWorld.screenSize.Y);




        Color color;
        byte dist = 12;
        int offsetX;
        int offsetY;
        private Texture2D sprite;
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("CollisionTexture ");
            trumpGraph = content.Load<Texture2D>("trumpGraph");

            trumpSad = content.Load<Texture2D>("trumpSad");

            //offsetX = (int)GameWorld.screenSize.X - RoomManager.roomLayout.GetLength(0) * sprite.Width - RoomManager.roomLayout.GetLength(0) * dist;
            //offsetY = (int)GameWorld.screenSize.Y - RoomManager.roomLayout.GetLength(1) * sprite.Width - RoomManager.roomLayout.GetLength(0) * dist;
            offsetX = 450;
            offsetY = 110;



            wSprite = GameWorld.player.currentWeapon.UISprite;

            UIBox276 = content.Load<Texture2D>("UIBox276");
            boxPosition = new Vector2((GameWorld.screenSize.X / 2) - (UIBox276.Width/2), GameWorld.screenSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kState = Keyboard.GetState();
            weapon = GameWorld.player.currentWeapon;
            if (kState.IsKeyDown(Keys.Tab) && boxPosition.Y >= GameWorld.screenSize.Y - UIBox276.Height)
            {
                boxPosition.Y -= 650 * dt;
            }
            if (kState.IsKeyUp(Keys.Tab) && boxPosition.Y <= GameWorld.screenSize.Y)
            {
                boxPosition.Y += 650 * dt;
            }
            if (GameWorld.player.IsAlive == false)
            {
                trumpVector.Y -= 465 * dt;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(UIBox276, new Vector2(boxPosition.X, boxPosition.Y), Color.White);
            spriteBatch.Draw(trumpGraph, new Rectangle((int)boxPosition.X - 10, (int)boxPosition.Y + 140, 300, 300), Color.White);
            if (wSprite != null)
            {
                if (GameWorld.player.currentWeapon is Throwable)
                {
                    spriteBatch.Draw(wSprite, new Rectangle((int)boxPosition.X + 70, (int)boxPosition.Y + 42, 30, 70), Color.White);
                }
                if (GameWorld.player.currentWeapon is Hitscan)
                {
                    spriteBatch.Draw(wSprite, new Rectangle((int)boxPosition.X + 70, (int)boxPosition.Y + 42, 70, 35), Color.White);
                }

                spriteBatch.DrawString(GameWorld.HUDFont, $"{GameWorld.player.currentWeapon.WName}", new Vector2(boxPosition.X + 70, boxPosition.Y + 8), Color.White);
            }
            spriteBatch.DrawString(GameWorld.HUDFont, $"Level:{RoomManager.levelsCleared}", new Vector2(boxPosition.X + 375, boxPosition.Y + 5), Color.White);
            spriteBatch.DrawString(GameWorld.HUDFont, $"Rooms Cleared:{RoomManager.roomsCleared}", new Vector2(boxPosition.X + 375, boxPosition.Y + 30), Color.White);


            if (GameWorld.player.IsAlive == false)
            {
                spriteBatch.Draw(trumpSad, new Vector2(trumpVector.X, trumpVector.Y), Color.White);
            }
            mapDisplay(spriteBatch);
        }

       



        public void mapDisplay(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < RoomManager.roomLayout.GetLength(1); y++)
            {
                for (int x = 0; x < RoomManager.roomLayout.GetLength(0); x++)
                {
                    if (RoomManager.playerInRoom[0] == x && RoomManager.playerInRoom[1] == y) color = Color.Green;
                    else if (RoomManager.roomLayout[x, y] == 5) color = Color.Black;
                    else if (RoomManager.roomLayout[x, y] >= 2) color = Color.Red;
                    else color = Color.White;

                    if (RoomManager.revealedRoom[x, y] || RoomManager.roomLayout[x, y] == 5)
                    {
                        spriteBatch.Draw(sprite, new Vector2((int)boxPosition.X + offsetX + (sprite.Width * x + dist * x), (int)boxPosition.Y + offsetY + (sprite.Height * y + dist * y)), color);
                        //boxPosition is the whereabouts of the TAB MENU. offset shifts the map to the correct place of the TAB MENU
                        //Next the sprite dimensions is multiplied by the index position of the map tile and dist for spacing inbetween.
                    }
                }
            }
        }
    }
}
