using Microsoft.Xna.Framework;
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
        public static bool HCDebug = false;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static List<GameObject> gameObjects;
        private static List<GameObject> deleteObjects;
        private static List<Enemy> enemies;
        public static List<GameObject> newObjects;
        public static List<GameObject> projectiles;

        public static Player player;
        
        public static Enemy enemy;

        public static RoomManager roomManager;
        public static Door door;

        public static UserInterface ui;


        public static SpriteFont HUDFont;

        private Texture2D cursor;

        private Texture2D collisionTexture;


        private Song music;


        public static Vector2 screenSize;

        

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;

            player = new Player();
            roomManager = new RoomManager();
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }


   

        public void RemoveObject(GameObject go)
        {

        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy();
            gameObjects.Add(enemy);
        }

        protected override void Initialize()
        {
            // _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            player = new Player();

            

            ui = new UserInterface();


            player.Position = new Vector2(500, 500);

            gameObjects = new List<GameObject>();
            newObjects = new List<GameObject>();
            projectiles = new List<GameObject>();
           // enemies = new List<Enemy>();

            deleteObjects = new List<GameObject>();
            gameObjects.Add(player);

            

            AddGameObject(new Enemy());


      
            //gameObjects.Add(new Revolver());

            for (byte i = 0; i < 4; i++) // Create the 4 doors. GameObject will handle LoadContent() and Update().
            {
                door = new Door(i);
                gameObjects.Add(door);
            }

            roomManager.CreateMap(9);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            HUDFont = Content.Load<SpriteFont>("HUDFont");

            cursor = Content.Load<Texture2D>("crosshair");

            collisionTexture = Content.Load<Texture2D>("CollisionTexture ");

            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);
            }
            ui.LoadContent(Content);
            roomManager.LoadContent(Content);




        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateGameObjects(gameTime);

            player.Update(gameTime);
            ui.Update(gameTime);

            base.Update(gameTime);
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }
            }
            //ui.mapDisplay();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            roomManager.DrawRoom(_spriteBatch);
            _spriteBatch.Draw(cursor, new Vector2(player.MousePosition.X , player.MousePosition.Y ), null, Color.Red);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);

                DrawCollisionBox(go);

            }

            
            _spriteBatch.DrawString(HUDFont, $"Health:  {player.currentHealth}/100", new Vector2(15, 10), Color.White);
            _spriteBatch.DrawString(HUDFont, $"Armor:   {player.currentHealth}/50", new Vector2(15, 32), Color.White);

            ui.mapDisplay(_spriteBatch);
            ui.Draw(_spriteBatch);



            _spriteBatch.End();


            base.Draw(gameTime);



        }

        /// <summary>
        /// Initializes game object by loading its contents and adding to the list
        /// </summary>
        /// <param name="gameObject"></param>
        private void AddGameObject(GameObject gameObject)
        {

            if (gameObject is null)
                throw new System.ArgumentNullException($"{nameof(gameObject)} cannot be null.");

            gameObject.LoadContent(this.Content);
            newObjects.Add(gameObject);


        }

        /// <summary>
        /// Instantiates GameObjects by adding them to the newObjects list.
        /// </summary>
        /// <param name="go"></param>
        public static void Instantiate(GameObject go)
        {

            newObjects.Add(go);
        }

        /// <summary>
        /// Despawns objects by adding them to the deleteObjects list.
        /// </summary>
        /// <param name="go"></param>
        public static void Despawn(GameObject go)
        {
            deleteObjects.Add(go);
        }

        /// <summary>
        /// is here only to make the real Update more readable, so updates about GameObjects goes here.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateGameObjects(GameTime gameTime)
        {

            foreach (var go in newObjects)
            {//has to be here to give projectiles a sprite before they are added to gameObjects and then drawn.
                go.LoadContent(this.Content);
            }

            gameObjects.AddRange(newObjects);
            newObjects.Clear();
            

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }


            }




            foreach (GameObject go in deleteObjects)
            {
                gameObjects.Remove(go);
            }
            deleteObjects.Clear();

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


    }
}
