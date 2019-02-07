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
                if (keyboardState.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }
            if (vector.Y <= window.ClientBounds.Height -
               texture.Height
               && vector.Y >= 0) ;
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.Up))
                    vector.Y -= speed.Y;
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
                    Bullet temp = new Bullet(bulletTexture, vector.X +
                        texture.Width / 2, vector.Y);
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
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }
        public int Points { get { return points; } set { points = value; } }


    }
    //skapa skott
    class Bullet : PhysicalObject
    {
        //skapa skott objetkt  
        public Bullet(Texture2D texture, float X, float Y)
       : base(texture, X, Y, 0f, 3f)
        {
        }
        //bullet uppdate
        public void Update()
        {
            vector.Y -= speed.Y;
            if (vector.Y < 0)
                isAlive = false;
        }
    }

}

