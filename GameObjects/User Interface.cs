using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class User_Interface : GameObject
    {

        private Weapon weapon;
        private Texture2D wSprite;
        private Texture2D UIBox1280;
        private Texture2D UIBox680;
        private Texture2D UIBox680360;
        private Vector2 boxPosition = new Vector2((GameWorld.screenSize.X/4), GameWorld.screenSize.Y);
        private int roomsCleared = 4;

        public User_Interface()
        {

        }


        public override Rectangle Collision
        {
            get
            {

                {
                    return new Rectangle(1, 1, 1, 1);

                }
            }
        }
        public override void LoadContent(ContentManager content)
        {
            wSprite = GameWorld.player.currentWeapon.Sprite;
            UIBox1280 = content.Load<Texture2D>("UIBox1280");
            UIBox680 = content.Load<Texture2D>("UIBox680");
            UIBox680360 = content.Load<Texture2D>("UIBox680360");
        }

        public override void OnCollision(GameObject other)
        {

        }

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(UIBox680360, new Vector2(boxPosition.X, boxPosition.Y), Color.White);
            if (wSprite != null)
            {
                spriteBatch.Draw(wSprite, new Rectangle((int)boxPosition.X+70, (int)boxPosition.Y+32, 65, 65), Color.White);
                spriteBatch.DrawString(GameWorld.HUDFont, $"{GameWorld.player.currentWeapon.Name}", new Vector2(boxPosition.X+70, boxPosition.Y+8), Color.White);
            }
            spriteBatch.DrawString(GameWorld.HUDFont, $"Rooms Cleared:{roomsCleared}", new Vector2(boxPosition.X + 375, boxPosition.Y + 5), Color.White);
        }
    }
}
    