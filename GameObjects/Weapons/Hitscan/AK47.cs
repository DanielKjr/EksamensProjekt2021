using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class AK47 : Hitscan
    {
        public AK47()
        {
            range = 700;
            damage = 5;
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/AK-47");
            origin = new Vector2(this.sprite.Width / 2, this.sprite.Height / 2);
            gunFire = content.Load<SoundEffect>("SoundEffects/SingleShot");
        }

    }
}
