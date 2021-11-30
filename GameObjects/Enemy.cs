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
        private Vector2 moveDir;


        private double fireRate;
        private double timer = 2;


    

        public Enemy()
        {
            
            Position = new Vector2(50,900);
            this.origin = Vector2.Zero;
            moveSpeed = 100;
        }

        public Enemy(Vector2 position)
        {

            position = Position;
            this.origin = Vector2.Zero;
            moveSpeed = 10;
        }

        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("Enemy2");

  
        }

        public override void Update(GameTime gameTime)
        {
            
           Movement(gameTime, PlayerPosition);


            EnemyFireRate(gameTime, PlayerPosition);
        }

        /// <summary>
        /// When called, and within range of player, this method shoots a projectile
        /// </summary>
        public  void Shoot(GameTime gameTime, Vector2 playerPosition)
        {
            if (Vector2.Distance(Position, PlayerPosition) < 500 )
            {
                GameWorld.Instantiate(new Projectile(sprite, Position));
   

            }
           
        }
        
        /// <summary>
        /// The method run in Update to make the enemy shoot, firing rate is determined by a timer depending on weapon
        /// </summary>
        /// <param name="gameTime"></param>
        public void EnemyFireRate(GameTime gameTime, Vector2 playerPosition)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                Shoot(gameTime, PlayerPosition);
                //TODO make use of the fireRate double - like fireRate = 1, then change the timer below this to be "timer = 2 - fireRate;" or something like that
                timer = 2;
               

            }

        }

        /// <summary>
        /// Moves the enemy towards the Player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="PlayerPosition"></param>
        public void Movement(GameTime gameTime, Vector2 playerPosition)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = PlayerPosition - this.Position;
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;

            //TODO make it so they stop for a moment when shooting before continuing

 


        }


     

        public override void OnCollision(GameObject other)
        {
            

        }

    }
}
