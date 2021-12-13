using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2021
{


    public class Enemy : GameObject
    {
        private Vector2 moveDir;        
        private Weapon weapon;
       
        GameObject playerPos = GameWorld.player;
        private double timer = 2;

        public Enemy() : base()
        {

            //enemy skal have et våben, lige nu er det bare Throwable men når vi får ting ind som en tomahawk ville det være new Tomahawk()
            this.weapon = new Tomahawk();


            //positionen som enemien spawner på
            Position = new Vector2(50, 900);

            //target, hvad enemien prøver at skyde efter og følger efter (den følger spilleren der er instantieret i GameWorld)
            target = playerPos.Position;
            
            this.origin = Vector2.Zero;

            //Movespeed, hvor hurtig de skal bevæge sig
            moveSpeed = 100;

            //hvor meget liv de har 
            health = 10;
        }

        //Den her constructor gør det nemt at tilføje enemies, ved brug af GameWorlds AddGameObject skal man bare give den en position man vile have
        //den position kan være en random, og et våben som så skal være et eller andet throwable våben.
        //man kan fint tilføje flere parametre til at sætte moveSpeed eller health hvis man har lyst til det
        //Men for at tilføje Enemien mens spillet kører skal vi bruge AddGameObject i GameWorld
        //Fordi den tilføjer Enemien til newObjects og kører dens LoadContent for at give sprite osv.
        //Eksempel på hvordan det kan bruges:
        //AddGameObject(new Enemy(new Vector2(200, 100), new Throwable()));
        public Enemy(Vector2 Position, Weapon weapon)
        {
            this.Position = Position;
            this.weapon = weapon;
            target = playerPos.Position;
            moveSpeed = 50;
            health = 10;
        }



        public override void LoadContent(ContentManager content)
        {

            sprite = content.Load<Texture2D>("Enemy2");
            //skal have en Weapon.LoadContent(Content); for at kunne loade våbnets sprite, samme går for spilleren når vi når dertil
            weapon.LoadContent(content);

        }

        public override void Update(GameTime gameTime)
        {
            
            EnemyTargeting(gameTime);
            Movement(gameTime);
            
        }

       

        /// <summary>
        /// should only run in Update, updates the weapons position, which follows the Enemy, and the players position.
        /// if the player is within the weapons range it will start the timer, then fire and reset the timer to the weapons fireRate.
        /// </summary>
        /// <param name="gameTime"></param>
        public void EnemyTargeting(GameTime gameTime)
        {
            target = new Vector2(playerPos.Position.X - 20, playerPos.Position.Y - 20);

            weapon.Position = Position;

            if (Vector2.Distance(Position, playerPos.Position) < weapon.Range)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

                if (timer <= 0)
                {
                    //bruger våbnets ShootWeapon, på samme måde som med Player går det an på hvad våben de har, men de burde kun have throwable
                    weapon.ShootWeapon(target);
                    //sætter timeren til at være lig med våbnets FireRate
                    timer = weapon.FireRate;
                }
                       
              
            }

        }

        

        /// <summary>
        /// Moves the enemy towards the Player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="PlayerPosition"></param>
        public void Movement(GameTime gameTime)
        {
           
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveDir = playerPos.Position - new Vector2(Position.X + 50, Position.Y - 40);
            moveDir.Normalize();
            Position += moveDir * moveSpeed * deltaTime;

            //TODO make it so they stop for a moment when shooting before continuing




        }

 


        public override void OnCollision(GameObject other)
        {
            if (other is HitscanShoot)
            {
                
                
                if (health <= 0)
                {
                    GameWorld.Despawn(this);
                }
               

                GameWorld.Despawn(other);
            }

        }

       
    }
}
