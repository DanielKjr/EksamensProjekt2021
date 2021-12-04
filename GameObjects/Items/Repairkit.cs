using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace EksamensProjekt2021
{
    class Repairkit : Item
    {
        private int armorplus = 10;


        public int Armorplus { get => armorplus; set => armorplus = value; }



        //public Item (string medkit)
        //{
        //    healthplus = 20;
        //    type = medkit;
        //    Position = new Vector2(400, 400);
        //}

        public Repairkit(Vector2 Position)
        {

            this.position = Position;

        }




        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("repairkit");
        }


        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }


}


