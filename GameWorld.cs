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

        private static List<GameObject> gameObjects;
        private static List<GameObject> deleteObjects;
        private static List<Enemy> enemies;
        private static List<Projectile> projectiles;

        Player player = new Player();

        private Texture2D cursor;

        private Texture2D collisionTexture;



        private Texture2D trumpWalkRight;
        private Texture2D trumpWalkLeft;
        private Texture2D trumpWalkUp;
        private Texture2D trumpWalkDown;

        private Song music;




        public static Vector2 screenSize;
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }



        public void RemoveObject(GameObject go)
        {

        }


        protected override void Initialize()
        {
            // _graphics.IsFullScreen = true;
            // TODO: Add your initialization logic here
            player.playerPosition = new Vector2(500, 500);

            gameObjects = new List<GameObject>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            deleteObjects = new List<GameObject>();
            AddGameObject(new Enemy());
            gameObjects.Add(player);
            
            



            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            MapGenerator(7);
            /*for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[x, y]+" ");
                }
                Console.WriteLine("");
            }*/
            

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
                
               // DrawCollisionBox(go);
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

            foreach (Player p in gameObjects.OfType<Player>())
            {

                foreach (Enemy e in gameObjects.OfType<Enemy>())
                {

                    e.PlayerPosition = p.Position;
                    e.Update(gameTime);
                    p.Update(gameTime);
                }
                foreach (Projectile proj in projectiles)
                {
                    proj.Update(gameTime);
                    proj.PlayerPosition = p.Position;
                }

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


        public byte[,] map = new byte[5, 5];
        private void MapGenerator(byte amountOfRooms)
        {
            for (int x = 0; x < map.GetLength(0); x++) // Reset map. Bruges til at lave nye levels.
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = 0;
                }
            }
            int[] index = new int[2] { 2, 2 };
            Random rnd = new Random();
            map[2, 2] = 1; //Set spawn room at 2,2 (middle of map)
            byte createdRooms = 0;
            byte filledRooms = 0;
            while (filledRooms < amountOfRooms)
            {
                for (int x = 0; x < map.GetLength(0); x++)//Find next index point
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        if (map[x, y] == 255)
                        {
                            index[0] = x;
                            index[1] = y;
                            map[index[0], index[1]] = RoomChance(); //See what this room should become
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
                        if (map[index[0] + x, index[1]] == 0) //Check if empty
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    map[index[0] + x, index[1]] = 255; //Fill temporary value into room
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
                        if (map[index[0], index[1] + y] == 0) //Check if clear
                        {
                            switch (rnd.Next(0, 3)) //Randomize if there should be a room in that pos
                            {
                                case 0:
                                    break; //Room not chosen
                                case 1:
                                    map[index[0], index[1] + y] = 255; //Fill temporary value into room
                                    createdRooms++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            map[index[0], index[1]] = 5; //Set last created room to be boss room
        }
        private byte RoomChance()
        {
            Random rnd = new Random();
            switch ((int)rnd.Next(0,101))
            {
                case int n when (n>-1 && n<15): //15% chance for loot
                    return 4;
                case int n when (n > 75 && n < 101)://25% chance for hard room
                    return 3;
                default:                       //60% chance for normal room
                    return 2;
            }
        }
    }
}
