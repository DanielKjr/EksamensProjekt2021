using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class AK47 : Hitscan
    {
        public AK47()
        {
            range = 400;
            damage = 5;
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/AK-47");
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");
        }

    }
}
