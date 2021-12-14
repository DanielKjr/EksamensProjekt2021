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
            damage = 6;
            

        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/MP5");


            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");

        }
    }
}
