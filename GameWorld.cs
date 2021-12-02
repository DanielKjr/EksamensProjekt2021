﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EksamensProjekt2021
{
    enum Dir
    {
        Right,
        Left,
        Up,
        Down
    }
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static List<GameObject> gameObjects;
        private static List<GameObject> deleteObjects;
        private static List<Enemy> enemies;
        private static List<Projectile> projectiles;

        public static Player player;
        public static Enemy enemy;
        public static GameObject target;

        private Texture2D cursor;

        private Texture2D collisionTexture;



        private Texture2D trumpWalkRight;
        private Texture2D trumpWalkLeft;
        private Texture2D trumpWalkUp;
        private Texture2D trumpWalkDown;

        private Song music;

        public static byte[,] roomLayout = new byte[5, 5];
        public static byte[,] roomDecorations = new byte[5, 5]; //Decorations within the room.
        public static byte mapReruns = 0; //See how many times the RoomsGenerator needed to run


        public static Vector2 screenSize;
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            player = new Player();
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }



        public void RemoveObject(GameObject go)
        {

        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(player);
            gameObjects.Add(enemy);
        }

        protected override void Initialize()
        {
            // _graphics.IsFullScreen = true;
            // TODO: Add your initialization logic here
            player = new Player();
            player.Position = new Vector2(500, 500);

            gameObjects = new List<GameObject>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            deleteObjects = new List<GameObject>();
            //AddGameObject(new Enemy());
            AddEnemy();
            gameObjects.Add(player);
            
            



            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            RoomsGenerator(9);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            collisionTexture = Content.Load<Texture2D>("CollisionTexture ");

            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);
            }

            foreach (Projectile go in projectiles)
            {
                go.LoadContent(this.Content);
            }



            trumpWalkRight = Content.Load<Texture2D>("trumpWalkRight");
            trumpWalkLeft = Content.Load<Texture2D>("trumpWalkLeft");
            trumpWalkUp = Content.Load<Texture2D>("trumpWalkUp");
            trumpWalkDown = Content.Load<Texture2D>("trumpWalkDown");
          
            player.animations[0] = new SpriteAnimation(trumpWalkRight, 6, 10); // SpriteAnimation(texture2D texture, int frames, int fps) forklaret hvad de gør i SpriteAnimation.cs
            player.animations[1] = new SpriteAnimation(trumpWalkLeft, 6, 10);
            player.animations[2] = new SpriteAnimation(trumpWalkUp, 6, 10);
            player.animations[3] = new SpriteAnimation(trumpWalkDown, 6, 10);
            //enum kan castes til int, så derfor kan vi bruge et array til at skife imellem dem. forklaret i player og hvor det relevant

            player.anim = player.animations[0]; //ændre sig afhængig af direction i player

            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            UpdateGameObjects(gameTime);
            
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);

                DrawCollisionBox(go);
                //den kan ikke finde ud af at tegne player rectangle lige nu så den er disabled
            }
            foreach (Projectile go in projectiles)
            {
                go.Draw(_spriteBatch);
            }

           
            
            
             //   player.anim.Draw(_spriteBatch);
            
          //  player.anim.Draw(_spriteBatch); //vi bruger Draw metoden i den SpriteAnimation "anim" som vi lavede på playeren. det ser fucking nice ud fordi det er så simpelt
          //jeg efterlader dem lige her indtil videre men jeg har overrided draw i player for at gøre det samme


            _spriteBatch.End();


            base.Draw(gameTime);



        }

        private void AddGameObject(GameObject gameObject)
        {

            if (gameObject is null)
                throw new System.ArgumentNullException($"{nameof(gameObject)} cannot be null.");

            gameObject.LoadContent(this.Content);
            gameObjects.Add(gameObject);


        }
        private void AddPlayer(GameObject gameObject)
        {
            //måske ikke nødvendig
            if (gameObject is null)
                throw new System.ArgumentNullException($"{nameof(gameObject)} cannot be null.");

            gameObject.LoadContent(this.Content);
            


        }

        public static void Instantiate(Projectile go)
        {
            projectiles.Add(go);
        }

        public static void Despawn(Projectile go)
        {
            deleteObjects.Add(go);
        }

        public void UpdateGameObjects(GameTime gameTime)
        {
            gameObjects.AddRange(projectiles);
            projectiles.Clear();

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

            }
            foreach (GameObject go in deleteObjects)
            {
                gameObjects.Remove(go);
            }

        }

        private void DrawCollisionBox(GameObject go)
        {
           
            
            Rectangle topLine = new Rectangle(go.Collision.X, go.Collision.Y, go.Collision.Width, 1);
            Rectangle bottomLine = new Rectangle(go.Collision.X, go.Collision.Y + go.Collision.Height, go.Collision.Width, 1);
            Rectangle rightLine = new Rectangle(go.Collision.X + go.Collision.Width, go.Collision.Y, 1, go.Collision.Height);
            Rectangle leftLine = new Rectangle(go.Collision.X, go.Collision.Y, 1, go.Collision.Height);

            _spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, leftLine, Color.Red);
        }

        /// <summary>
        /// Generates the map based on amount of rooms desired.
        /// Will auto retry if it fails (random WILL fail)
        /// </summary>
        /// <param name="amountOfRooms"></param>
        private void RoomsGenerator(byte amountOfRooms)
        {
            RoomsReset();
            Random rnd = new Random();
            byte createdRooms = 0;
            byte filledRooms = 0;
            byte failSafe = 0; //If the code messes up, use this to escape

            byte rndX = (byte)rnd.Next(1, 4);
            byte rndY = (byte)rnd.Next(1, 4);
            roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
            int[] index = new int[2] { rndX, rndY, }; //Sets index to spawn room.


            while (filledRooms < amountOfRooms)
            {
                for (int x = 0; x < roomLayout.GetLength(0); x++)//Find next index point
                {
                    for (int y = 0; y < roomLayout.GetLength(1); y++)
                    {
                        if (roomLayout[x, y] == 255)
                        {
                            index[0] = x;
                            index[1] = y;
                            roomLayout[index[0], index[1]] = RoomChance(); //See what this room should become
                            roomDecorations[index[0], index[1]] = (byte)rnd.Next(0, 10); //Tell room to have random decoration style. 0-9 exists.
                            filledRooms++;
                            break;
                        }
                    }
                }



                if (createdRooms < amountOfRooms) //Only create a new room, if there is a need for it.
                {
                    for (int x = -1; x <= 1; x += 2) //Check left and right
                    {
                        if (index[0] + x == -1 || index[0] + x == 5) break; //Check if outside
                        if (roomLayout[index[0] + x, index[1]] == 0) //Check if empty
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    roomLayout[index[0] + x, index[1]] = 255; //Fill temporary value into room
                                    createdRooms++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    for (int y = -1; y <= 1; y += 2) //Check up and down
                    {
                        if (index[1] + y == -1 || index[1] + y == 5) break; //Check if outside
                        if (roomLayout[index[0], index[1] + y] == 0) //Check if clear
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    roomLayout[index[0], index[1] + y] = 255; //Fill temporary value into room
                                    createdRooms++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                failSafe++;
                if (failSafe >= 100) //If the map failed, run again
                {
                    createdRooms = 0;
                    filledRooms = 0;
                    failSafe = 0;
                    RoomsReset();
                    rndX = (byte)rnd.Next(1, 4);
                    rndY = (byte)rnd.Next(1, 4);
                    roomLayout[rndX, rndY] = 1;  //Sets inital spawn room
                    index = new int[2] { rndX, rndY, }; //Sets index to spawn room.
                    mapReruns++;
                }
            }
            roomLayout[index[0], index[1]] = 5; //Set last created room to be boss room


            //RoomDebug(failSafe, mapReruns);
        }
        /// <summary>
        /// Chances of the different rooms
        /// Returns which room it should be
        /// </summary>
        /// <returns></returns>
        private byte RoomChance()
        {
            Random rnd = new Random();
            switch ((int)rnd.Next(0, 101))
            {
                case int n when (n > -1 && n < 15): //15% chance for loot
                    return 4;
                case int n when (n > 75 && n < 101)://25% chance for hard room
                    return 3;
                default:                       //60% chance for normal room
                    return 2;
            }
        }
        /// <summary>
        /// Console.WriteLine debug method
        /// </summary>
        /// <param name="failSafe"></param>
        /// <param name="reruns"></param>
        private void RoomDebug(byte failSafe, byte reruns)
        {
            Console.Clear();
            for (int y = 0; y < roomLayout.GetLength(0); y++)
            {
                for (int x = 0; x < roomLayout.GetLength(1); x++)
                {
                    if (roomLayout[x, y] == 0) Console.ForegroundColor = ConsoleColor.White;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(roomLayout[x, y] + " ");
                }
                if (failSafe >= 100) Console.WriteLine("  BROKE");
                else Console.WriteLine("");
            }
            Console.WriteLine($"\nReruns: {reruns}");
            mapReruns = 0;
        }
        /// <summary>
        /// Clears Map[] array.
        /// </summary>
        private void RoomsReset()
        {
            for (int x = 0; x < roomLayout.GetLength(0); x++) // Reset map. Bruges til at lave nye levels.
            {
                for (int y = 0; y < roomLayout.GetLength(1); y++)
                {
                    roomLayout[x, y] = 0;
                }
            }
        }
        /// <summary>
        /// Draws decorations in the room.
        /// Needs RoomDecorations[,] for style
        /// </summary>
        /// <param name="style"></param>
    }
}
