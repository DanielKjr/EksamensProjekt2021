using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class Item : GameObject
    {
        public List<Item> items = new List<Item>();

        protected int nextWeapon;
        protected bool show;
        protected byte xByte;
        protected byte yByte;
        public int NextWeapon { get => nextWeapon; set => nextWeapon = value; }




        public override void LoadContent(ContentManager content)
        {
           
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (RoomManager.playerInRoom[0] == xByte && RoomManager.playerInRoom[1] == yByte)
            {
                show = true;
            }
            else show = false;
        }
    }
}
