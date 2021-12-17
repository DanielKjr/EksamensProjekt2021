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
        public WeaponPickup(Vector2 Position)
        {
            nextWeapon = rnd.Next(0, 4);
            this.position = Position;
            xByte = (byte)this.position.X;
            yByte = (byte)this.position.Y;

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
                
                default:
                    sprite = content.Load<Texture2D>("Weapons/MP5");
                    weapon = new MP5();
                    weapon.LoadContent(content);
                    break;
            }

        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

                GameWorld.player.NewWeapon(weapon);
                GameWorld.Despawn(this);

            }
        }
    }
}
