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
        private Weapon weapon;
        bool isAlive = true;
        GameObject playerPos;
        
        private double timer = 2;

        public Enemy() : base()
        {
            GameWorld.aaa++;
            this.weapon = new Throwable();

            this.playerPos = GameWorld.player;

            Position = new Vector2(50, 900);

            target = playerPos.Position;
            
            this.origin = Vector2.Zero;
            moveSpeed = 100;
            health = 10;
        }




        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("Enemy2");
            //skal have en Weapon.LoadContent(Content); for at kunne loade våbnets sprite, samme går for spilleren når vi når dertil
            weapon.LoadContent(content);

        }

        public override void Update(GameTime gameTime)
        {
            
            EnemyTargeting(gameTime);
            Movement(gameTime);
            
        }

       

        /// <summary>
        /// should only run in Update, updates the weapons position, which follows the Enemy, and the players position.
        /// if the player is within the weapons range it will start the timer, then fire and reset the timer to the weapons fireRate.
        /// </summary>
        /// <param name="gameTime"></param>
        public void EnemyTargeting(GameTime gameTime)
        {
            target = new Vector2(playerPos.Position.X - 20, playerPos.Position.Y - 20);

            weapon.Position = Position;

            if (Vector2.Distance(Position, playerPos.Position) < weapon.Range)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

                if (timer <= 0)
                {
                    weapon.ShootWeapon(target);

                    timer = weapon.FireRate;
                }
                       
              
            }

        }

        

        /// <summary>
        /// Moves the enemy towards the Player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="PlayerPosition"></param>
        public void Movement(GameTime gameTime)
        {
           
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = playerPos.Position - new Vector2(Position.X + 50, Position.Y - 40);
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;

            //TODO make it so they stop for a moment when shooting before continuing




        }

        public void Damage()
        {
           // health -= damage;
            if (health <= 0)
            {

                GameWorld.Despawn(this);

                isAlive = false;
            }

        }


        public override void OnCollision(GameObject other)
        {
            if (other is HitscanShoot)
            {
                Damage();
                GameWorld.Despawn(other);
            }

        }

       
    }
}
