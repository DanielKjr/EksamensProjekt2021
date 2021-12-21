using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{
    class WeaponPickup : Item
    {
        Weapon weapon;
        Random rnd = new Random();

        public override Rectangle Collision
        {
            get
            {


                return new Rectangle(
               (int)(Position.X),
               (int)(Position.Y),
               sprite.Width,
               sprite.Height
               );


            }
        }
        public WeaponPickup(Vector2 Position, byte x, byte y)
        {
            nextWeapon = rnd.Next(0, 5);
            this.Position = Position;
            xByte = x;
            yByte = y;

        }


        public override void LoadContent(ContentManager content)
        {
            switch (nextWeapon)
            {
                case 0:
                    {
                        sprite = content.Load<Texture2D>("Weapons/AK-47");
                        weapon = new AK47();
                        weapon.LoadContent(content);
                       
                        break;
                    }
                case 1:
                    {
                        sprite = content.Load<Texture2D>("Weapons/M16");
                        weapon = new M16();
                        weapon.LoadContent(content);
                       
                        break;
                    }
                case 2:
                    {
                        sprite = content.Load<Texture2D>("Weapons/lilleTomahawk");
                        weapon = new Tomahawk();
                        weapon.LoadContent(content);
                       
                        break;
                    }
                case 3:
                    {

                        sprite = content.Load<Texture2D>("Weapons/MP5");
                        weapon = new MP5();
                        weapon.LoadContent(content);
                        
                        break;
                    }
                case 4:
                    {
                        sprite = content.Load <Texture2D>("snub-nosedRevolver");
                        weapon = new Revolver();
                        weapon.LoadContent(content);
                        break;
                    }


            }

        }

        public override void Update(GameTime gameTime)
        {
            if (RoomManager.playerInRoom[0] == xByte && RoomManager.playerInRoom[1] == yByte)
            {
                show = true;
            }
            else show = false;
        }


        public override void OnCollision(GameObject other)
        {
            if (other is Player && show)
            {

                GameWorld.player.NewWeapon(weapon);
                GameWorld.Despawn(this);

            }
        }
    }
}
