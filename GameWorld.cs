using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

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

        private int[] map;




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

            gameObjects = new List<GameObject>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            deleteObjects = new List<GameObject>();
            AddGameObject(new Enemy());

             AddPlayer(new Player());
            //new Player();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

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
            player.Update(gameTime);
            UpdateGameObjects(gameTime);
            


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
            }
            foreach (Projectile go in projectiles)
            {
                go.Draw(_spriteBatch);
            }
            player.anim.Draw(_spriteBatch); //vi bruger Draw metoden i den SpriteAnimation "anim" som vi lavede på playeren. det ser fucking nice ud fordi det er så simpelt

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




    }
}
