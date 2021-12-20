using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class Revolver : Hitscan
    {


        public Revolver()
        {
           

            damage = 3;
            range = 500;
            fireRate = 1.2;
            moveSpeed = 200;

        }




        public override void LoadContent(ContentManager content)
        {
            
            sprite = content.Load<Texture2D>("snub-nosedRevolver");

            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");

        }

    }
}
