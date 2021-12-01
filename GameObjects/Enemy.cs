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

        GameObject target;
        //en vector2 position er en struct så den opdateres aldrig (hvor mindre vi gør det i update) så vi skal have en reference/klasse
        private double fireRate;
        private double timer = 2;

        //prøver stadig at lave noget så ignorer indtil videre
        public GameObject Target { get => target; set => target = value; }

        public Enemy() : base()
        {
            this.target = GameWorld.player;

            Position = new Vector2(50, 900);
            this.origin = Vector2.Zero;
            moveSpeed = 100;
        }

        public Enemy(GameObject target)
        {
            Position = new Vector2(50, 900);
            moveSpeed = 100;
            this.target = GameWorld.player;
        }

        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("Enemy2");


        }

        public override void Update(GameTime gameTime)
        {



            Movement(gameTime);
            EnemyFireRate(gameTime);




        }

        /// <summary>
        /// When called, and within range of player, this method shoots a projectile
        /// </summary>
        public void Shoot(GameTime gameTime, GameObject target)
        {

            if (Vector2.Distance(this.Position, target.Position) < 500)
            {
                GameWorld.Instantiate(new Projectile(sprite, Position, target));


            }

        }

        /// <summary>
        /// The method run in Update to make the enemy shoot, firing rate is determined by a timer depending on weapon
        /// </summary>
        /// <param name="gameTime"></param>
        public void EnemyFireRate(GameTime gameTime)
        {

            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                Shoot(gameTime, target);
                //TODO make use of the fireRate double - like fireRate = 1, then change the timer below this to be "timer = 2 - fireRate;" or something like that
                timer = 2;


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
            moveDir = target.Position - this.Position;
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;

            //TODO make it so they stop for a moment when shooting before continuing




        }




        public override void OnCollision(GameObject other)
        {


        }

    }
}
