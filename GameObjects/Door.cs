using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    public class Door : GameObject //Conjoined with RoomManager
    {
        byte dir;
        Vector2 placementDir;
        bool showDoor;
        SpriteEffects effect = SpriteEffects.None; //Needed for right door
        Texture2D storedSprite;

        int width;
        byte height;

        float animTimer;
        int fpsThreshold = 1; //Seconds between frames
        Rectangle[] source = new Rectangle[4];
        byte frameIndex = 0;



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
                    storedSprite = content.Load<Texture2D>("DoorTop"); //0
                    animSetup();
                    position = new Vector2(GameWorld.screenSize.X / 2 - width / 2, 0);
                    placementDir = new Vector2(0, -1);
                    break;
                case 1:
                    storedSprite = content.Load<Texture2D>("DoorTop"); //1
                    animSetup();
                    position = new Vector2(GameWorld.screenSize.X / 2 - width / 2, GameWorld.screenSize.Y - height);
                    placementDir = new Vector2(0, 1);
                    effect = SpriteEffects.FlipVertically;
                    break;
                case 2:
                    storedSprite = content.Load<Texture2D>("DoorTop"); //2
                    animSetup();
                    position = new Vector2(0, GameWorld.screenSize.Y / 2 - storedSprite.Height / 2);
                    placementDir = new Vector2(-1, 0);
                    break;
                case 3:
                    storedSprite = content.Load<Texture2D>("DoorTop"); //3
                    animSetup();
                    position = new Vector2(GameWorld.screenSize.X - width, GameWorld.screenSize.Y / 2 - height / 2);
                    placementDir = new Vector2(1, 0);
                    effect = SpriteEffects.FlipHorizontally;
                    break;
            }
        }
        /// <summary>
        /// Create all data needed for spriteSheet
        /// </summary>
        void animSetup()
        {
            width = storedSprite.Width / 4;
            height = (byte)storedSprite.Height;
            for (int i = 0; i < 4; i++)
            {
                source[i] = new Rectangle(width * i, 0, width, height);
            }
            sprite = storedSprite; //DONT DO THIS---------------------------------------
        }
        /// <summary>
        /// Needs custom rect since its a spriteSheet
        /// </summary>
        public override Rectangle Collision
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, width, height);
            }
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
            if (showDoor) //Animation section.
            {
                animTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animTimer >= fpsThreshold)
                {
                    if (frameIndex < 3) frameIndex++;
                    animTimer = 0;
                }
            }
            if (!showDoor) frameIndex = 0;
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
                if (placementDir.X == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X - width * 2, GameWorld.screenSize.Y / 2);
                if (placementDir.X == 1) GameWorld.player.Position = new Vector2(width * 2, GameWorld.screenSize.Y / 2);
                if (placementDir.Y == -1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y - height * 2);
                if (placementDir.Y == 1) GameWorld.player.Position = new Vector2(GameWorld.screenSize.X / 2, height * 2);
                RoomManager.playerInRoom[0] += (byte)placementDir.X; //Sets player room pos to new room
                RoomManager.playerInRoom[1] += (byte)placementDir.Y;
                GameWorld.roomManager.Debug(0, 0);
            }
        }
        /// <summary>
        /// Custom draw. Needed showDoor bool to see if the door should be rendered.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, source[frameIndex], Color.White, 0, origin, 1, effect, 0);
        }
    }
}