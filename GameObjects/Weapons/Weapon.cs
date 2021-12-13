using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
  
    public abstract class Weapon : GameObject
    {
        private string wName;
        protected int range;
        protected byte damage;
        protected int fireRate;
        protected float rotation;
        protected SpriteEffects weaponMirror;

       

        protected SoundEffect gunFire;

        public int Range { get => range;  }
        public int FireRate { get => fireRate;  }
        public float Rotation { get => rotation; set => rotation = value; }
        public byte Damage { get => damage; }

        public SpriteEffects WeaponMirror { get => weaponMirror; set => weaponMirror = value; }
        public SoundEffect GunFire { get => gunFire;}

        public Texture2D UISprite { get => sprite; }
        public string WName { get => GetType().Name;  }

        public Weapon()
        {
           
        }



        public override void OnCollision(GameObject other)
        {


        }

        /// <summary>
        /// Override to allow the weapon to have it's own rotation 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin, 1, weaponMirror, 0);

        }



        /// <summary>
        /// The shoot function depends on the weapon type, the Player/Enemy needs a weapon to access this function
        /// and is then used by weapon.ShootWeapon(target);
        /// </summary>
        public abstract void ShootWeapon(Vector2 target);




        public override void Update(GameTime gameTime)
        {
            

        }
    }
}
