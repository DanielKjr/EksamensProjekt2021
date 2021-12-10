﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
     class Projectile : Throwable
    {



        public Projectile(Vector2 position, Vector2 target, byte damage)
        {
            this.damage = damage;
            
            Position = position;
            this.target = target;
            this.origin = Vector2.Zero;

            moveSpeed = 400;
        }


        /// <summary>
        /// The animation/movement of the projectile from the position of the shooter to the target position.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="playerPosition"></param>
        public void ProjectileShoot(GameTime gameTime, Vector2 target)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 shootDir = target - Position;
            shootDir.Normalize();
            Position += shootDir * moveSpeed * deltaTime;

        }



        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("medkit");

        }


        public override void Update(GameTime gameTime)
        {

            ProjectileShoot(gameTime, target);

            if (Vector2.Distance(Position, target) < 10)
            {
                GameWorld.Despawn(this);
                //TODO add damage to player
            }
            
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player || other is Enemy)
            {
                //lige nu despawner den projektiler når den der instantiere det skyder
               // GameWorld.Despawn(this);
            }
        }




    }
}