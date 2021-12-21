using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{


    public class Enemy : GameObject
    {
        protected Vector2 moveDir;

        private Weapon weapon;
        protected GameObject playerPos = GameWorld.player;

        private double timer = 2;


        public Enemy() 
        {
                      
        }

        public Enemy(Vector2 Position, Weapon weapon)
        {
            GameWorld.EnemyCount++;
            this.Position = Position;
            this.weapon = weapon;
            target = playerPos.Position;
            moveSpeed = 50;
            health = 10;
            weapon.CanHurtPlayer = true;
        }



        public override void LoadContent(ContentManager content)
        {
            Random rnd = new Random();
            sprites = new Texture2D[7];

            sprites[0] = content.Load<Texture2D>("Enemies/Ninja");
            sprites[1] = content.Load<Texture2D>("Enemies/Police");
            sprites[2] = content.Load<Texture2D>("Enemies/Karen");
            sprites[3] = content.Load<Texture2D>("Enemies/Biden2");
            sprites[4] = content.Load<Texture2D>("Enemies/American");
            sprites[5] = content.Load<Texture2D>("Enemies/Gangster");
            sprites[6] = content.Load<Texture2D>("Enemies/Chinese");

            int i = rnd.Next(0, 7);
            sprite = sprites[i];

            weapon.LoadContent(content);

        }

        public override void Update(GameTime gameTime)
        {
            
            EnemyTargeting(gameTime);
            Movement(gameTime);
           

            if (health <= 0)
            {
                GameWorld.EnemyCount--;
                GameWorld.Despawn(this);
            }

        }




        /// <summary>
        /// should only run in Update, updates the weapons position, which follows the Enemy, and the players position.
        /// if the player is within the weapons range it will start the timer, then fire and reset the timer to the weapons fireRate.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void EnemyTargeting(GameTime gameTime)
        {
            target = new Vector2(playerPos.Position.X - 20, playerPos.Position.Y - 20);
            weapon.Position = Position;
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (Vector2.Distance(Position, playerPos.Position) < weapon.Range)
            {

                if (timer <= 0)
                {
                    //bruger våbnets ShootWeapon, på samme måde som med Player går det an på hvad våben de har, men de burde kun have throwable
                    weapon.ShootWeapon(target);
                    //sætter timeren til at være lig med våbnets FireRate
                    timer = weapon.FireRate;


                }


            }

        }

     

        /// <summary>
        /// Moves the enemy towards the Player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="PlayerPosition"></param>
        public virtual void Movement(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = playerPos.Position - new Vector2(Position.X + 50, Position.Y - 40);
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;



        }

      


        public override void OnCollision(GameObject other)
        {


        }


    }
}
