using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class BidenBox : Biden
    {

        protected Vector2 ballotTarget = new Vector2(-1, 0);
        
        protected SpriteEffects weaponMirror;
        
        private float ballotRotation;
        private Random rand;

        public BidenBox(Vector2 position, Vector2 target, byte damage, float throwRotationSpeed)
        {
            this.damage = damage;

            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;
            this.throwRotationSpeed = throwRotationSpeed;
            
            moveSpeed = 750;
            

        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Box");
            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
        }

        public void BidenBoxShoot(GameTime gameTime, Vector2 target)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

        }
        public override void Update(GameTime gameTime)
        {

            BidenBoxShoot(gameTime, target);
            
            if (Vector2.Distance(Position, target) < 10)
            {

                BallotShoot(gameTime, ballotTarget);
                GameWorld.Despawn(this);
                //TODO add damage to player
            }
            throwRotation += throwRotationSpeed;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {



            }

        }
        public void BallotShoot(GameTime gametime, Vector2 ballotTarget)
        {
            rand = new Random();
            for (int i = 0; i < 4; i++)
            {               
                ballotTarget = new Vector2(playerPos.Position.X + (int)rand.Next(-75, 75), playerPos.Position.Y + (int)rand.Next(-75, 75));
                Vector2 bRotate = ballotTarget - position;
                BallotRotation = (float)Math.Atan2(bRotate.Y, bRotate.X);
                GameWorld.Instantiate(new BidenBallot(position, ballotTarget, damage, BallotRotation));                
            }





        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, throwRotation, origin, 1, weaponMirror, 0);

        }
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

        public float BallotRotation { get => ballotRotation; set => ballotRotation = value; }
    }
}
