using Microsoft.Xna.Framework;
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
        protected int fireRate;
        protected float rotation;
        protected SpriteEffects weaponMirror;

        
        public int Range { get => range; set => range = value; }
        public int FireRate { get => fireRate; set => fireRate = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public byte Damage { get => damage; set => damage = value; }

        public SpriteEffects WeaponMirror { get => weaponMirror; set => weaponMirror = value; }
        public Vector2 Origin { get => origin; set => origin = value; }

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
