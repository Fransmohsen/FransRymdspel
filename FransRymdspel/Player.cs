using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FransRymdspel
{
    class Player : PhysicalObject
    {

        List<Bullet> bullets; //alla skott
        Texture2D bulletTexture; //skott bild
        double timeSinceLastBullet = 0; //i millisekunder
        
        
        public List<Bullet> Bullets { get { return bullets; } }
        int points = 0;


        public Player(Texture2D texture, float X, float Y,
            float speedX, float speedY, Texture2D bulletTexture)
             : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
            
            
        }


        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //flytta rymdskeppet efter tangettryckningar(om den inte går utanför kanten)
            if (vector.X <= window.ClientBounds.Width -
                texture.Width
                && vector.X >= 0) 
            {
                if (keyboardState.IsKeyDown(Keys.D))
                    angle -= 0.1f;
                if (keyboardState.IsKeyDown(Keys.A))
                    angle += 0.1f;
                

            }
              var direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(0) - angle), -(float)Math.Sin(MathHelper.ToRadians(0) - angle));


            if (vector.Y <= window.ClientBounds.Height -
               texture.Height
               && vector.Y >= 0) 
            {
                if (keyboardState.IsKeyDown(Keys.S))
                    vector -= direction*speed.Y;

                if (keyboardState.IsKeyDown(Keys.W))
                    vector += direction * speed.Y;
                if (keyboardState.IsKeyDown(Keys.Q))
                    vector += direction * speed.Y;
            }
            //vänster
            if (vector.X < 0)
                vector.X = 0;

            //höger
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width -
                    texture.Width;
            }

            //upp
            if (vector.Y < 0)
                vector.Y = 0;


            //ner
            if (vector.Y > window.ClientBounds.Height - texture.Height) 
            {
                vector.Y = window.ClientBounds.Height -
                    texture.Height;
            }

            // spelaren vill skjuta
            if(keyboardState.IsKeyDown(Keys.Space))
            {
                //kontrollera om spelaren får skjuta
                if (gameTime.TotalGameTime.TotalMilliseconds >
                    timeSinceLastBullet + 200) 
                {
                    //skapa skottet
                    Bullet temp = new Bullet(bulletTexture, vector.X /*- texture.Width / 4 +
                          texture.Height / 4*/, vector.Y
                          , direction);


                    bullets.Add(temp); //lägg skott i listan
                    //sätt TimeSinceLastBullet till detat ögonblicket:
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;

                }
            }
            //flytta på alla skott
            foreach(Bullet b in bullets.ToList())
            {
                //flytta på skottet:
                b.Update();
                //kontrollera att skottet inte är dött
                if (!b.IsAlive)
                    bullets.Remove(b); //ta bort skottet ur listan
            }


 

            
        }
        public override void  Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, null, Color.White, angle + (float)Math.PI / 2,
                    new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, 0);

            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }
        public int Points { get { return points; } set { points = value; } }


    }
    //skapa skott
    class Bullet : PhysicalObject
    {
        Vector2 direction;
        
       
       


        //skapa skott objetkt  
        public Bullet(Texture2D texture, float X, float Y, Vector2 direction)
       : base(texture, X, Y, 3f, 3f)
        {
            this.direction = direction;
           
            
            
            
            
        }
        //bullet uppdate
        public void Update()
        {
            vector+=direction*speed;
            if (vector.Y < 0)
                isAlive = false;
        }
    }

}

