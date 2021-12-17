using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class BidenBox : BossWeapon
    {


        public override Rectangle Collision
        {
            get
            {


                return new Rectangle(
                               (int)(Position.X - 48),
                               (int)(Position.Y - 48),
                               this.sprite.Width,
                               this.sprite.Height
                               );


            }
        }
 

        public BidenBox()
        {

        }
        public BidenBox(Vector2 position, Vector2 target, byte damage, float throwRotationSpeed)
        {
            this.damage = damage;
            fireRate = 3;
            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;
            this.throwRotationSpeed = throwRotationSpeed;
            
            moveSpeed = 650;
            

        }

      

        public void BidenBoxShoot(GameTime gameTime, Vector2 target)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

        }

        public void BallotShoot(GameTime gametime, Vector2 target)
        {
            rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                ballotTarget = new Vector2(target.X + (int)rand.Next(-125, 125), target.Y + (int)rand.Next(-125, 125));
                Vector2 bRotate = ballotTarget - position;
                BallotRotation = (float)Math.Atan2(bRotate.Y, bRotate.X);
                GameWorld.Instantiate(new BidenBallot(position, ballotTarget, damage, BallotRotation));

            }



        }
        public override void Update(GameTime gameTime)
        {
           // GameWorld.BidenHealth = health;
            BidenBoxShoot(gameTime, target);

            if (Vector2.Distance(Position, target) < 10)
            {


                BallotShoot(gameTime, target);
                GameWorld.Despawn(this);

            }
            throwRotation += throwRotationSpeed;


        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

                GameWorld.player.Damage(1);
               // GameWorld.Despawn(this);
            }

        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Box");
            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
        }

       

      

       
    }
}
