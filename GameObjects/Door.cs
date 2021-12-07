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
        byte dir;
        Vector2 placementDir;
        bool showDoor;
        SpriteEffects effect = SpriteEffects.None; //Needed for right door
        public SpriteAnimation doorAnim;
        


        /// <summary>
        /// 0 = up, 1 = down, 2 = left, 3 = right
        /// </summary>
        /// <param name="placement"></param>
        public Door(byte placement)
        {
            this.dir = placement;
        }
        /// <summary>
        /// Load doors. Done in GameWorld.Initialize.
        /// Takes directional value to know which door should be loaded. (up, down, left, or right door)
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            switch (dir)
            {
                case 0:
                    sprite = content.Load<Texture2D>("DoorTop"); //0
                    position = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, 0);
                    placementDir = new Vector2(0, -1);
                    break;
                case 1:
                    sprite = content.Load<Texture2D>("SpritePlaceHolder2"); //1
                    position = new Vector2(GameWorld.screenSize.X / 2 - sprite.Width / 2, GameWorld.screenSize.Y - sprite.Height);
                    placementDir = new Vector2(0, 1);
                    break;
                case 2:
                    sprite = content.Load<Texture2D>("DoorSides"); //2
                    position = new Vector2(0, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    placementDir = new Vector2(-1, 0);
                    break;
                case 3:
                    sprite = content.Load<Texture2D>("DoorSides"); //3
                    position = new Vector2(GameWorld.screenSize.X - sprite.Width, GameWorld.screenSize.Y / 2 - sprite.Height / 2);
                    placementDir = new Vector2(1, 0);
                    effect = SpriteEffects.FlipHorizontally;
                    break;
            }
            doorAnim = new SpriteAnimation(sprite, 4, 5);
        }
        /// <summary>
        /// Updates if the door should be shown. Best if it runs every frame despite potential lag.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            showDoor = false;
            if (RoomManager.roomLayout[RoomManager.playerInRoom[0], RoomManager.playerInRoom[1]] <= 1) //checks if the room the player is in is empty
            {
                if (RoomManager.playerInRoom[0] + (int)placementDir.X > -1 && RoomManager.playerInRoom[0] + (int)placementDir.X < 5) //Out of bounds X check
                {
                    if (RoomManager.playerInRoom[1] + (int)placementDir.Y > -1 && RoomManager.playerInRoom[1] + (int)placementDir.Y < 5) //Out if bounds Y check
                    {
                        if (RoomManager.roomLayout[RoomManager.playerInRoom[0] + (int)placementDir.X, RoomManager.playerInRoom[1] + (int)placementDir.Y] >= 1)
                        { showDoor = true; } //If the room to the DOOR PLACEMENT DIRECTION is clear and exists, show door
                    }
                }
            }
        }
        /// <summary>
        /// When player hits the door, check if they should be moved to next room.
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {
            if (other is Player && showDoor) // Only allow collision if the door is active
            {
                //Change player pos to match entering new room from that door
                if (placementDir.X == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X - sprite.Width * 3, GameWorld.screenSize.Y / 2);
                if (placementDir.X == 1) GameWorld.player.Position = new Vector2(sprite.Width * 3, GameWorld.screenSize.Y / 2);
                if (placementDir.Y == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, sprite.Height * 3);
                if (placementDir.Y == 1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y - sprite.Height * 3);
                RoomManager.playerInRoom[0] += (byte)placementDir.X; //Sets player room pos to new room
                RoomManager.playerInRoom[1] += (byte)placementDir.Y;
                GameWorld.roomManager.Debug(0,0);
            }
        }
        /// <summary>
        /// Custom draw. Needed showDoor bool to see if the door should be rendered.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (showDoor)spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, effect, 0);
        }
    }
}
