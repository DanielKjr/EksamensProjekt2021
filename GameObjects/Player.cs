using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Player : GameObject
    {


        public Player()
        {
            Position = new Vector2(0, 0);
            Position = PlayerPosition;
        }



        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enemy2");
        }

        public override void OnCollision(GameObject other)
        {
 
        }

        public override void Shoot()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
