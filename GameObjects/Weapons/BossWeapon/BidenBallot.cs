using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class BidenBallot : BidenBox
    {

        public override Rectangle Collision
        {
            get
            {
                return new Rectangle(
                               (int)(Position.X - 8),
                               (int)(Position.Y - 17),
                               sprite.Width,
                               sprite.Height
                               );

            }
        }

        public BidenBallot(Vector2 position, Vector2 target, byte damage, float BallotRotation)
        {
            
            
            Position = position;
            this.target = target;
            origin = Vector2.Zero;
            ballotRotation = BallotRotation;
            
            moveSpeed = 300;


        }

       
        public void BidenBallotShoot(GameTime gameTime, Vector2 target)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

        }

        public override void Update(GameTime gameTime)
        {

            BidenBallotShoot(gameTime, target);

            if (Vector2.Distance(Position, target) < 10)
            {
                GameWorld.Despawn(this);
               
            }
            
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("BallotRight");
            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

                GameWorld.player.Damage(5);
                GameWorld.Despawn(this);
            }

        }
       
        
  

        
    }
}

