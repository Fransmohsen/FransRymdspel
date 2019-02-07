using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FransRymdspel
{
    //Goldcoin mynt om ger poöng
    class GoldCoin : PhysicalObject
    {
        double timeToDie; //hur länge den ska leva
        // för att skapa objectet
        public GoldCoin(Texture2D texture, float X, float Y,
            GameTime gameTime)
            :base (texture,X,Y,0,2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }
        //uppdate
        public void Update(GameTime gameTime)
        {
            //döda guldmyntet om den är för gammal
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                IsAlive = false ;
         }
        
    }
}
