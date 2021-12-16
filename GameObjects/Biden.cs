using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Biden : Enemy
    {
        public SpriteAnimation anim;
        private Texture2D biden;
        private Vector2 bidenDestination = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y / 2);
        private Random rand = new Random();
        protected byte damage = 15;
        protected float throwRotation = 0f;
        protected float throwRotationSpeed;
        private float fireRate = 3;
        private float timer = 3;
        private int bidenEnemyX;
        private int bidenEnemyY;
        private bool dontTouch = false;
        private float dontTouchTextTimer = 5;
        private float dontTouchTimer = 10;
        
        //TODO biden kan ikke dø lige pt, fordi at hans projektiler er alle sammen enemies og det resetter hans liv hver gang han angriber og enemy counteren går op forevigt så man kan ikke "vinde" 
        //så en af de to under klasser skal nok laves til at våben og så den anden en underklasse af det våben, ellers så er alle 3 dele en Enemy 
        public Biden()
        {
            Position = new Vector2(800, 500);
            moveSpeed = 500;
            health = 200;
            target = playerPos.Position;
        }

        public override void LoadContent(ContentManager content)
        {
            biden = content.Load<Texture2D>("biden");
            anim = new SpriteAnimation(biden, 5, 9);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                dontTouch = true;
                

            }
        }
        public void BidenEnemySpawn(GameTime gameTime)
        {
            dontTouchTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            dontTouchTextTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (dontTouch == true && dontTouchTimer <= 0)
            {
                
                bidenEnemyX = (rand.Next(150, (int)GameWorld.screenSize.X - 100));
                bidenEnemyY = (rand.Next(150, (int)GameWorld.screenSize.Y - 100));
                GameWorld.Instantiate(new Enemy(new Vector2(bidenEnemyX, bidenEnemyY), new Tomahawk()));
                dontTouchTimer = 10;
                dontTouchTextTimer = 1.5f;
                
                
            }
            dontTouch = false;
        }
        public override void Movement(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = bidenDestination - new Vector2(Position.X - 50, Position.Y - 70);
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;
            if (Vector2.Distance(position, bidenDestination) < 200)
            {
                bidenDestination = new Vector2(rand.Next(50, (int)GameWorld.screenSize.X - 50), rand.Next(50, (int)GameWorld.screenSize.Y - 50));  
            }


        }
        public override void Update(GameTime gameTime)
        {
            
            anim.Update(gameTime);
            anim.Position = new Vector2(Position.X - 50, Position.Y - 70);
            Movement(gameTime);
            BidenShoot(gameTime);
            BidenEnemySpawn(gameTime);
            if (health <= 0)
            {
                GameWorld.Despawn(this);
                GameWorld.bossSpawned = false;
            }
        }

        public void BidenShoot(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= deltaTime;
            target = new Vector2(playerPos.Position.X - 20, playerPos.Position.Y - 20);
            if (timer <= 0)
            {
                GameWorld.Instantiate(new BidenBox(Position, Target, damage, 0.2f));
                timer = fireRate;
            }
            
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            anim.Draw(spriteBatch);
            
            if (dontTouchTextTimer >= 0)
            {
                spriteBatch.DrawString(GameWorld.HUDFont, "Don't Touch My Master!", new Vector2(bidenEnemyX - 205, bidenEnemyY + 90), Color.White);
            }
            
        }
        public override Rectangle Collision
        {
            get
            {


                return new Rectangle(
                               (int)(anim.Position.X),
                               (int)(anim.Position.Y),
                               100,
                               140
                               );


            }
        }
    }
}
