using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class MP5 : Hitscan
    {
        public MP5()
        {
            range = 500;
            damage = 5;
            
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/MP5");
        }
    }
}
