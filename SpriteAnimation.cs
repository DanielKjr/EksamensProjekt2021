using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    public class SpriteManager
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;

        public SpriteManager(Texture2D Texture, int frames)
        {
            this.Texture = Texture;
            int width = Texture.Width / frames;
            Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, Texture.Height);
            //lader os vælge noget indenfor en specific kasse inden i det spritesheet vi har. virker kun sidelæns fordi jeg ikke er gud. "frames" hentyder til hvor mange frames der er på en række
            // i spritesheet'et og Texture hentyder til selve sprite-sheetet. den regner selv ud hvor lang spritesheetet er ved at divider Texture.Width med mængden af frames der er deri. 
            // frames er en værdi vi selv giver den når vi opretter den
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
            //den her draw tegner vores den nuværende animation. nogen af de værdier her er her kun fordi eksemplet jeg så med det os har dem. de 4 første og Origin er vigtige for at det fungere.
            //  spriteEffect tror jeg er en helt anden ting man kan dykke ned i. scale og rotation siger selv og er inkluderet fordi de lyder at rare at ha og SpriteBatch bare har den funktion.
        }
    }

    public class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate; 
        public int FramesPerSecond { set { timeToUpdate = (1f / value); } }
        //de 3 værdier her bruger vi til at skife frame, så det er animeret. det er sat op sådan at vi let kan ændre tallene på hvad end vi bruger spriteManager'en på ved at sætte vores ønskede
        //"fps" når vi konstruere vores spriteAnimation ude i gameworld's load (eller hvor vi nu ender med at ha det). Propertien "FramesPerSecond" er en property til TimeToUpdate, og er 
        // nød til at være der sårn vi kan bruge den nede i vores update

        public SpriteAnimation(Texture2D Texture, int frames, int fps) : base(Texture, frames)
        {
            // texture = spritesheetet, frames = alle forskellige frames det ska bruge, fps = hvor hurtigt det skifter imellem dem. spriteAnimation'en inheriter texture og frames fra spriteManager
            //og det er så fucking cool at den gør det
            FramesPerSecond = fps;
        }

        public void Update(GameTime gameTime)
        {
            //alle updates ska ha gametime for at opdater.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //delta time sårn den er akkurate uanset perfomance
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;
                // så den går igennem og stopper før vi ville få en index out of range-error
                else if (IsLooping)
                    FrameIndex = 0;
                // sårn at den går tilbage til den originale frame når har løbet dem igennem. gør at den looper
            }
        }

        public void setFrame(int frame)
        {
            FrameIndex = frame;
            //tvinger den til at være den frame vi vil ha. 
        }
    }
}