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
        protected int range;
        protected byte damage;
        protected double fireRate;
        protected float rotation;
        protected SpriteEffects weaponMirror;
        protected bool canHurtPlayer;
        protected SoundEffect gunFire;

        public string WName { get => GetType().Name; }
        public byte Damage { get => damage; }
        public float MoveSpeed { get => moveSpeed; }
        public float Rotation { get => rotation; set => rotation = value; }
        public double FireRate { get => fireRate;  }
        public int Range { get => range; }
        public bool CanHurtPlayer { get => canHurtPlayer; set => canHurtPlayer = value; }
        public SpriteEffects WeaponMirror { get => weaponMirror; set => weaponMirror = value; }
        public SoundEffect GunFire { get => gunFire;}
        public Texture2D UISprite { get => sprite; }

       



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
