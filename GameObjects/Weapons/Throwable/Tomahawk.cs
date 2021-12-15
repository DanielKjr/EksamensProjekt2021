using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Tomahawk : Throwable
    {
        
        public Tomahawk()
        {
            range = 700;
            damage = 10;
            throwRotationSpeed = 6f;
            fireRate = 2;
            moveSpeed = 250;
        }

        public Tomahawk(bool canHurtPlayer)
        {
            range = 700;
            damage = 15;
            throwRotationSpeed = 6f;
            fireRate = 1;
            this.canHurtPlayer = canHurtPlayer;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/lilleTomahawk");


            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");

        }
    }
}
