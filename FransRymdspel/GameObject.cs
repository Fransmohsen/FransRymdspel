using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FransRymdspel
{
   class  GameObject
    {
        protected Texture2D texture;
        protected Vector2 vector;
        protected float angle = 0;


        //konstruktor för att skapa objekt
        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;

        }

        //draw ritar ut bildskärm
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        //Egenskaper för gameObject
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float width { get{ return texture.Width; } }
        public float height { get { return texture.Height; } }


    }
    //Klass för objekt som rör sig
    abstract class MovingObject : GameObject
    {
        protected Vector2 speed; //hastighet
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
        

    }
    abstract class PhysicalObject : MovingObject
    {
        protected bool isAlive = true;   //spelare object

        //Constructor
        public PhysicalObject(Texture2D texture, float X, float Y
            , float speedX,
            float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        //Kolla kollisioner
        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle MyRect = new Rectangle(Convert.ToInt32(X),
                Convert.ToInt32(Y), Convert.ToInt32(width),
                Convert.ToInt32(height));
            Rectangle otherRect =
                new Rectangle(Convert.ToInt32(other.X),
                Convert.ToInt32(other.Y), Convert.ToInt32(other.width),
                Convert.ToInt32(other.height));
            return MyRect.Intersects(otherRect);
        }

        //egenskaper för PhysicalObject
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }


    }
}
