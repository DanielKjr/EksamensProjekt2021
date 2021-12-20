using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2021
{
    public class SpriteManager
    {
        public Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;
        public Rectangle sourceA;
        public Color TextureData;
        public SpriteManager(Texture2D Texture, int frames)
        {
            this.Texture = Texture;
            int width = Texture.Width / frames;
            Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle(i * width, 0, width, Texture.Height);

            }
            
            //lader os vælge noget indenfor en specific kasse inden i det spritesheet vi har. Der er muglighed for, at den også tager højde for højde og man kun behøver et sprite-sheet.
            //"frames" hentyder til hvor mange frames der er på en række
            // i spritesheet'et og Texture hentyder til selve sprite-sheetet. den regner selv ud hvor lang spritesheetet er ved at divider Texture.Width med mængden af frames der er deri. 
            // frames er en værdi vi selv giver den når vi opretter den


        }
        public void SetPosition(Vector2 Position)
        {
            this.Position = Position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
            //den her draw tegner vores den nuværende animation. nogen af de værdier her er her kun sådan, at vi kan bruge dem hvis vi finder ud af vi skal senere. de 4 første og Origin er vigtige
            //for at det fungere.
            
        }
    }

    public class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate;
        public int FramesPerSecond { set { timeToUpdate = (1f / value); } }
        //de 3 værdier her bruger vi til at skife frame, så det er animeret. det er sat op sådan at vi let kan ændre tallene på hvad end vi bruger spriteManager'en på ved at sætte vores ønskede
        //"fps" når vi instantiere vores spriteAnimation ude i gameworld's load (eller hvor vi nu ender med at ha det). Propertien "FramesPerSecond" er en property til TimeToUpdate, og er 
        // nød til at være der sårn vi kan bruge den nede i vores update
        
        public SpriteAnimation(Texture2D Texture, int frames, int fps) : base(Texture, frames)
        {
            FramesPerSecond = fps;
            // texture = spritesheetet, frames = alle forskellige frames det ska bruge, fps = hvor hurtigt det skifter imellem dem. spriteAnimation'en inheriter texture og frames fra spriteManager
            //og det er så fucking cool at den gør det

        }
        //public void GetTextureData()
        //{

        //    Color[] textureData = new Color[63 * 71];
        //    Texture.GetData<Color>(0, Rectangles[FrameIndex], textureData, 0, Texture.Width);
        //    TextureData = textureData;
        //} dette Stykke kode er her for at tilgå informationen på den nuværende frame på en anden måde, men endte med at være overflødig
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