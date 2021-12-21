using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class MP5 : Hitscan
    {
        public MP5()
        {
            range = 500;
            damage = 2;          
            moveSpeed = 200;
            fireRate = 1;
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/MP5");


            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");

        }
    }
}
