using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FransRymdspel
{
    abstract class Enemy : PhysicalObject

    {
        //för att kolla om fiende lever eller inte

        // skapa objektet
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY)
           : base(texture, X, Y, speedX, speedY)
        {
        }
        //uppdaterar fiender postion
        public abstract void Update(GameWindow window);
    }


        //mine som rör sig höger vänster
        class Mine : Enemy
        {
            public Mine(Texture2D texture,float X,float Y)
                :base(texture,X,Y,6f,0.3f)
            {
            }
            //uppdatera att finder postion
            public override void Update(GameWindow window)
            {
                //flytta fiender
                vector.X += speed.X;
                //kontrollera att den inte åker ut
                if (vector.X > window.ClientBounds.Width - texture.Width ||
                    vector.X < 0)
                    speed.X *= -1; //byt riktning på fiende
                vector.Y += speed.Y;
                //gör fienden inaktiv om den åker där nere
                if (vector.Y > window.ClientBounds.Height)
                    isAlive = false;

            }
            //egenskaper för Enemy

        }
    //tripod, en annan fiende
    class Tripod: Enemy
    {
        public Tripod(Texture2D texture, float X, float Y)
                :base(texture,X,Y,0f,3f)
        {
        }
        //update fiendens postion
        public override void Update(GameWindow window)
        {
            //flytta på fienden
            vector.Y += speed.Y;
            //gör fienden inaktiv om den åker ut där nere
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;
        }
    }
}
        

