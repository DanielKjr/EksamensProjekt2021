using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class BossWeapon : Weapon
    {
        protected float throwRotation = 0f;
        protected float throwRotationSpeed;
        protected float ballotRotation;

        protected Vector2 ballotTarget = new Vector2(-1, 0);

      
        protected Random rand;
        public float BallotRotation { get => ballotRotation; set => ballotRotation = value; }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void ShootWeapon(Vector2 target)
        {
            this.target = target;
            GameWorld.Instantiate(new BidenBox(Position, target, damage, 0.2f));
            
           
        }


        public override void Update(GameTime gameTime)
        {
            
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, throwRotation, origin, 1, weaponMirror, 0);

        }
    }
}
