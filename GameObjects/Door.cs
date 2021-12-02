using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    public class Door : GameObject //Conjoined with RoomManager
    {
        Vector2 pos;
        Texture2D sprite;
        byte dir;
        Vector2 placementDir;
        bool showDoor;
        /// <summary>
        /// 0 = up, 1 = down, 2 = left, 3 = right
        /// </summary>
        /// <param name="placement"></param>
        public Door(byte placement)
        {
            this.dir = placement;
        }


        public override void LoadContent(ContentManager content)
        {
            switch (dir)
            {
                case 0:
                    sprite = content.Load<Texture2D>("Door0");
                    pos = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, 0);
                    placementDir = new Vector2(0, -1);
                    break;
                case 1:
                    sprite = content.Load<Texture2D>("Door1");
                    pos = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, GameWorld.screenSize.Y - sprite.Height);
                    placementDir = new Vector2(0, 1);
                    break;
                case 2:
                    sprite = content.Load<Texture2D>("Door2");
                    pos = new Vector2(0, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    placementDir = new Vector2(-1, 0);
                    break;
                case 3:
                    sprite = content.Load<Texture2D>("Door3");
                    pos = new Vector2(GameWorld.screenSize.X - sprite.Width, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    placementDir = new Vector2(1, 0);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (RoomManager.roomLayout[RoomManager.playerInRoom[0], RoomManager.playerInRoom[1]] <= 1) //checks if the room the player is in is empty
            {
                if (RoomManager.roomLayout[RoomManager.playerInRoom[0] + (int)placementDir.X, RoomManager.playerInRoom[1] + (int)placementDir.Y] <= 1)
                { showDoor = true; } //If the room to the DOOR PLACEMENT DIRECTION is clear, show door
            }
            else showDoor = false;
        }

        public override void OnCollision(GameObject other)
        {
        }
    }
}
