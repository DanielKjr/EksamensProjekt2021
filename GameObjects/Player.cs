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
    class Player : GameObject
    {

        private Vector2 position = new Vector2(500, 300);
        private int speed = 350;
        //en værdi vi bare kan ændre til at passe med hvor hurtigt vi vil ha ham. bliver brugt i udregninen af når han bevæger sig
        private bool isMoving = false;
        //forklaret hvor den er relevant
        private Dir direction = Dir.Right;
        // enum'en som vi lavede ude i gameworld
        private MouseState mStateOld = Mouse.GetState();
        //ska vi bruger senere til shoot-funktion. trust me boiis.

        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];
        // forklaret hvor den er relevant


        public Vector2 Position
        {
            get { return position; }
        }
        //property sårn vi ban benytte os af den




        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.Y = newY;
        }
        //er her fordi ellers så kommer der en error med den ikke kan finde ud af vector fordi det en data-type og ik en værdi


        public override void OnCollision(GameObject other)
        {


        }

        public override void Shoot()
        {

        }

        public override void Update(GameTime gameTime)
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
            anim = animations[(int)direction];
            anim.Position = new Vector2(position.X - 20, position.Y - 48); // ska ændres til at passe spriten

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
    }
}
