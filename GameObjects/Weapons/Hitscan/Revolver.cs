using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    class Revolver : Hitscan
    {


        public Revolver()
        {
            //insert damage, armor, magazine or what not here
            Name = "Revolver";
            range = 500;

        }




        public override void LoadContent(ContentManager content)
        {
            //the weapon sprite has to be loaded within the specific weapon class.
            sprite = content.Load<Texture2D>("medkit");
            Sprite = sprite;

        }

    }
}
