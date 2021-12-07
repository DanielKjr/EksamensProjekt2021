using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
   class Revolver : Hitscan
    {

        public Revolver() 
        {
            
        }
        public Revolver(Texture2D sprite, Vector2 Position, Vector2 target)
        {

            this.sprite = sprite;


        }

        public override void LoadContent(ContentManager content)
        {
            
           sprite = content.Load<Texture2D>("CollisionTexture ");
           
        }

    }
}
