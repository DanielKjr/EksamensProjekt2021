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

        private List<Enemy> enemies;
        private List<Projectile> projectiles;

        Player player = new Player();

        private Texture2D cursor;

        private Texture2D playerSprite;
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

        public void AddObject(GameObject go)
        {

        }

        public void RemoveObject(GameObject go)
        {

        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("SpritePlaceHolder2");
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
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();
            player.anim.Draw(_spriteBatch); //vi bruger Draw metoden i den SpriteAnimation "anim" som vi lavede på playeren. det ser fucking nice ud fordi det er så simpelt
            _spriteBatch.End();

        }

        

    }
}
