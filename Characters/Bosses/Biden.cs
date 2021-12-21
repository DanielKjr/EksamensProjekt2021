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
        private Weapon weapon;
        public SpriteAnimation anim;
        private Texture2D biden;
        private Vector2 bidenDestination = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y / 2);

        private Random rand = new Random();
       
        private int bidenEnemyX;
        private int bidenEnemyY;

        private float timer = 3;
        private float dontTouchTextTimer = 5;
        private float dontTouchTimer = 10;

        private bool dontTouch = false;
        public override Rectangle Collision
        {
            get
            {
                return new Rectangle(
                               (int)(anim.Position.X),
                               (int)(anim.Position.Y),
                               anim.Texture.Width / 5,
                               anim.Texture.Height
                               );
            }
        }


        public Biden()
        {
            Position = new Vector2(800, 500);
            moveSpeed = 500;
            health = 200;
            GameWorld.EnemyCount++;
            target = playerPos.Position;
            weapon = new BidenBox(Position, target, 5, 0.2f);
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
                GameWorld.Instantiate(new Enemy(new Vector2(bidenEnemyX, bidenEnemyY), new Tomahawk(true)));
                dontTouchTimer = 10;
                dontTouchTextTimer = 1.5f;
            }
            dontTouch = false;
            //denne metode spawner en fjender når spilleren rør Biden
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
            //Biden bevæger sig i random retninger i stedet for mod spilleren
        }

        public override void Update(GameTime gameTime)
        {
            GameWorld.BidenHealth = health;
            weapon.Position = Position;
            weapon.Update(gameTime);

            anim.Update(gameTime);
            anim.Position = new Vector2(Position.X , Position.Y);
            Movement(gameTime);
            BidenShoot(gameTime);


            BidenEnemySpawn(gameTime);

            if (health <= 0)
            {
                GameWorld.Despawn(this);
                GameWorld.bossSpawned = false;
                GameWorld.EnemyCount--;
            }
        }
        public void BidenShoot(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= deltaTime;
            target = new Vector2(playerPos.Position.X - 20, playerPos.Position.Y - 20);
            if (timer <= 0)
            {
                weapon.ShootWeapon(target);
                timer = (float)weapon.FireRate;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            anim.Draw(spriteBatch);

            if (dontTouchTextTimer >= 0 && GameWorld.EnemyCount >= 2)
            {
                spriteBatch.DrawString(GameWorld.HUDFont, "Don't Touch My Master!", new Vector2(bidenEnemyX - 205, bidenEnemyY + 90), Color.White);
            }
        }
        public override void LoadContent(ContentManager content)
        {
            weapon.LoadContent(content);
            biden = content.Load<Texture2D>("biden");
            anim = new SpriteAnimation(biden, 5, 9);
           
        }
    }
}
