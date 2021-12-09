using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class AK47 : Hitscan
    {
        
        public AK47()
        {
            range = 500;
            Name = "AK47";
        }


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Weapons/AK-47");
            Sprite = sprite;
        }

    }
}
