using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EksamensProjekt2021
{
    public class Player : GameObject
    {

        private Weapon weapon;

        // private MouseState mStateOld = Mouse.GetState();
        private MouseState mState;
        private Vector2 mousePosition;
        private bool mLeftReleased = true;
        


        private int speed = 250;

        //en værdi vi bare kan ændre til at passe med hvor hurtigt vi vil ha ham. bliver brugt i udregninen af når han bevæger sig
        private bool isMoving = false;
        //forklaret hvor den er relevant
        private Dir direction = Dir.Right;
        // enum'en som vi lavede ude i gameworld

        //ska vi bruger senere til shoot-funktion. trust me boiis.
       

        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];

        private Texture2D trumpWalkRight;
        private Texture2D trumpWalkLeft;
        private Texture2D trumpWalkUp;
        private Texture2D trumpWalkDown;


        public Weapon currentWeapon { get => weapon; }
        public int currentArmor { get => armor; }

        public int currentHealth { get => health; }

        public override Rectangle Collision
        {
            get
            {
                if (direction == Dir.Down || direction == Dir.Up)
                {
                    return new Rectangle(
                                   (int)(anim.Position.X),
                                   (int)(anim.Position.Y),
                                   43,
                                   71
                                   );
                }
                else
                {
                    return new Rectangle(
                   (int)(anim.Position.X),
                   (int)(anim.Position.Y),
                   63,
                   70
                   );
                }
            }

        }


        public Vector2 MousePosition { get => mousePosition; }


        public Player()
        {
            Position = new Vector2(500, 500);

            health = 100;
            armor = 50;

            weapon = new AK47();
            isAlive = true;

            PlayerPosition = position;

        }



        public override void Update(GameTime gameTime)
        {

            
                UpdateWeapon();
                PlayerShoot(gameTime);
                HandeInput(gameTime);

                PlayerAnimation(gameTime);
            

           

        }

        /// <summary>
        /// Updates weapon position, target and rotation.
        /// </summary>
        private void UpdateWeapon()
        {

            //set weapon position so it knows where to draw it
            weapon.Position = new Vector2(Position.X, Position.Y);

            //mstate to create mouse position
            mState = Mouse.GetState();
            mousePosition = new Vector2(mState.X - 20, mState.Y - 20);


            //weapon rotation, gets passed on to weapon.Rotation which is then drawn
            Vector2 wRotate = mousePosition - weapon.Position;
            weapon.Rotation = (float)Math.Atan2(wRotate.Y, wRotate.X);


            if (MousePosition.X > GameWorld.screenSize.X / 2)
            {
                weapon.WeaponMirror = SpriteEffects.None;
                direction = Dir.Right;
                weapon.Position = new Vector2(Position.X + 10, Position.Y);
            }
            else
            {
                weapon.WeaponMirror = SpriteEffects.FlipVertically;
                direction = Dir.Left;
                weapon.Position = new Vector2(Position.X - 10, Position.Y);

            }

        }

        /// <summary>
        /// Method is run in Update, it instantiates a projectile on click by using the current weapons' shoot function if the clicked position is in range.
        /// </summary>
        /// <param name="gameTime"></param>
        private void PlayerShoot(GameTime gameTime)
        {
            if (mState.LeftButton == ButtonState.Pressed && mLeftReleased == true && Vector2.Distance(Position, mousePosition) < weapon.Range)
            {

                mLeftReleased = false;
                weapon.ShootWeapon(mousePosition);

                weapon.GunFire.Play();

            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mLeftReleased = true;
            }
        }

        private void Damage(int damage)
        {
            if (armor != 0)
            {
                armor -= damage;

            }
            else
            {
                health -= damage;

                if (health <= 0)
                {

                    GameWorld.Despawn(this);
                    
                    isAlive = false;
                }
            }

        }
        public override void OnCollision(GameObject other)
        {
            if (other is Projectile)
            {
                Damage(weapon.Damage);
                GameWorld.Despawn(other);
            }
        }

        public void MedkitHeal(int Healthplus)
        {
            Health += Healthplus;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            anim.Draw(spriteBatch);

            weapon.Draw(spriteBatch);

        }

        private void HandeInput(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            //keyboard.getstate() ved statusen på vores keyboard (om vi har trykket på en knap eller givet slip for eksempel). kstate gemmer det til en variabel, sårn vi kan benytte os af den

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //delta-time fordi ellers så afhænger tiden af performance

            isMoving = false; // relevant fordi vi kun vil kører igennem vores animationer når vi bevæger os, så han ikke løber på stedet

            if (kState.IsKeyDown(Keys.D)) //IsKeyDown ser om en knap er trykket på vores kState. 
            {
                direction = Dir.Right; // ændre direction, sårn vi ka brugedet til at vælge hvilken animation vi vil bruge
                                       //if (position.X < 1920) ska finde en anden måde at begrænse det på
                position.X += speed * dt; //sådan at den bevæger sig
                isMoving = true; //forklaret ovenover
            }
            if (kState.IsKeyDown(Keys.A))
            {
                direction = Dir.Left;
                if (position.X > 0)
                    position.X -= speed * dt;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.W))
            {
                direction = Dir.Up;
                if (position.Y > 0)
                    position.Y -= speed * dt;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.S))
            {
                direction = Dir.Down;
                if (position.Y < 1080)
                    position.Y += speed * dt;
                isMoving = true;
            }
        }

        public void PlayerAnimation(GameTime gameTime)
        {
            anim = animations[(int)direction];
            anim.Position = new Vector2(Position.X - 20, Position.Y - 48); // ska ændres til at passe spriten

            if (isMoving)
            {
                anim.Update(gameTime);
            }
            else
            {
                if (direction == Dir.Right || direction == Dir.Up || direction == Dir.Right)
                {
                    anim.setFrame(1);
                }
                if (direction == Dir.Up)
                {
                    anim.setFrame(2);
                }
                // dette gør at hvis man ik bevæger sig, så sætter den animation til at være den bestemte frame tilsvarende på retning's spritesheet. synes de her er de bedste som idle frames. kan diskuteres
            }
        }
        public override void LoadContent(ContentManager content)
        {

            weapon.LoadContent(content);

            //  weapon.Origin = new Vector2(20,20);

            trumpWalkRight = content.Load<Texture2D>("trumpWalkRight");
            trumpWalkLeft = content.Load<Texture2D>("trumpWalkLeft");
            trumpWalkUp = content.Load<Texture2D>("trumpWalkUp");
            trumpWalkDown = content.Load<Texture2D>("trumpWalkDown");

            animations[0] = new SpriteAnimation(trumpWalkRight, 6, 8); // SpriteAnimation(texture2D texture, int frames, int fps) forklaret hvad de gør i SpriteAnimation.cs
            animations[1] = new SpriteAnimation(trumpWalkLeft, 6, 8);
            animations[2] = new SpriteAnimation(trumpWalkUp, 6, 8);
            animations[3] = new SpriteAnimation(trumpWalkDown, 6, 8);
            //enum kan castes til int, så derfor kan vi bruge et array til at skife imellem dem. forklaret i player og hvor det relevant
            anim = animations[0]; //ændre sig afhængig af direction i player

        }
    }
}
