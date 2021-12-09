using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class M16 : Hitscan
    {
        public M16()
        {
            range = 500;
            Name = "M16";
        }



        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/M16");
            Sprite = sprite;
        }
    }
}
