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

        private MouseState mStateOld = Mouse.GetState();
        private MouseState mState;
        public Vector2 mousePosition;
        private bool mLeftReleased = true;


        private int speed = 250;

        //en værdi vi bare kan ændre til at passe med hvor hurtigt vi vil ha ham. bliver brugt i udregninen af når han bevæger sig
        private bool isMoving = false;
        //forklaret hvor den er relevant
        private Dir direction = Dir.Right;
        // enum'en som vi lavede ude i gameworld
       
        //ska vi bruger senere til shoot-funktion. trust me boiis.
        static public bool isAlive = true;

        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];

        private Texture2D trumpWalkRight;
        private Texture2D trumpWalkLeft;
        private Texture2D trumpWalkUp;
        private Texture2D trumpWalkDown;






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

      

        public Player() 
        {
            Position = new Vector2(500, 500);

            
           
            

            this.weapon = new Revolver();


            //  this.weapon = new Hitscan();

            // this.weapon = new Throwable();


            PlayerPosition = position;
            Health = 100;

         

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
        public void UpdateWeapon()
        {
            //set weapon position so it knows where to draw it
            weapon.Position = new Vector2(Position.X + 10, Position.Y);

            //mstate to create mouse position
            mState = Mouse.GetState();
            mousePosition = new Vector2(mState.X + 15, mState.Y + 20);

            //weapon rotation, gets passed on to weapon.Rotation which is then drawn
            Vector2 wRotate = mousePosition - weapon.Position;
            weapon.Rotation = (float)Math.Atan2(wRotate.Y, wRotate.X);
        }

        /// <summary>
        /// Method is run in Update, it instantiates a projectile on click by using the current weapons' shoot function if the clicked position is in range.
        /// </summary>
        /// <param name="gameTime"></param>
        public void PlayerShoot(GameTime gameTime)
        {
          

            if (mState.LeftButton == ButtonState.Pressed && mLeftReleased == true && Vector2.Distance(Position, mousePosition) < weapon.Range)
            {
                
                mLeftReleased = false;
                weapon.ShootWeapon(mousePosition);
                
            }
           

            if (mState.LeftButton == ButtonState.Released)
            {
                mLeftReleased = true;
            }


        }

        public void Damage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {

                GameWorld.Despawn(this);

                isAlive = false;
            }

        }
        public override void OnCollision(GameObject other)
        {

        }


        public void MedkitHeal(int Healthplus)
        {
            Health += Healthplus;
        }


        public Player(Vector2 position)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            anim.Draw(spriteBatch);

            weapon.Draw(spriteBatch);

        }

        
        public void SetX(float newX)
        {
            position.X = newX;
        }
        public void SetY(float newY)
        {
            position.Y = newY;
        }
        

        //er her fordi ellers så kommer der en error med den ikke kan finde ud af vector fordi det en data-type og ik en værdi



        public void HandeInput(GameTime gameTime)
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
            //det burde ændre hvilken animation man bruger efter en prioritet der tilsvare hvornår man skrev den ind, men dette virker da fuck fuk fuck min fucking pik fuck det lort her fuck.
            /*
            if (isMoving)
            {
                switch (direction)
                {
                    case Dir.Right:
                        anim = animations[0];
                        break;
                    case Dir.Left:
                        anim = animations[1];
                        break;
                    case Dir.Up:
                        anim = animations[2];
                        break;
                    case Dir.Down:
                        anim = animations[3];
                        break;
                }

            } 
            unødvendig kode, fordi enums faktisk er brugbart after all. vi typecaster vores dir til en int, som vi ka bruge i vores array med vores retninger.
            det nedenunder gør essentielt det samme, men ser kloger ud. der må ikke fuckes med rækkefølgen af værdierne der hvor vi laver vores enum i gameWorld
            */

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
