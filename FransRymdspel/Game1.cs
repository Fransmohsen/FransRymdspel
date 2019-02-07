﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FransRymdspel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        List<Enemy> enemies;
        List<GoldCoin> goldCoins;
        Texture2D goldCoinsSprite;
        PrintText printText;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            goldCoins = new List<GoldCoin>();
            Random random = new Random();
            // TODO: Add your initialization logic here
         
            goldCoins = new List<GoldCoin>();
            player = new Player(Content.Load<Texture2D>("ship"),
                380, 400, 5f, 7f, Content.Load<Texture2D>("bullet"));
            base.Initialize();


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("ship"), 380, 400, 7.2f, 5.6f,
                Content.Load<Texture2D>("powerups/bullet"));
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite =
                Content.Load<Texture2D>("mine");
            for(int i=0; i<10; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width -
                    tmpSprite.Width);

                int rndY = random.Next(0, Window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp); //lägg till i listan
            }
            tmpSprite = Content.Load<Texture2D>("tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width -
                    tmpSprite.Width);

                int rndY = random.Next(0, Window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
              
                enemies.Add(temp); //lägg till i listan
            }





            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
            goldCoinsSprite = Content.Load<Texture2D>("powerups/coin");
            


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //uppdate
            Random random = new Random();
            int newCoin = random.Next(1, 200);
            if (newCoin==1) //ok, nytt guldmynt ska uppstå
            {
                //vart ska guldmyntet uppstå
                int rndX = random.Next(0, Window.ClientBounds.Width -
                    goldCoinsSprite.Width);
                int rndY= random.Next(0, Window.ClientBounds.Height -
                    goldCoinsSprite.Height);
                //lägg mynten i listan
                goldCoins.Add(new GoldCoin(goldCoinsSprite, rndX, rndY, gameTime));
            }
            //gå igenom hela listan med exsiterande mynten
            foreach(GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive)//kontrollera om guldmyntet lever
                {
                    //gc update
                    gc.Update(gameTime);
                    //kontrollera om de kolliderat med spelaren:
                    if (gc.CheckCollision(player))
                    {
                        //ta bort vid kollision:
                        goldCoins.Remove(gc);
                        player.Points++;
                    }
                }
                else
                    goldCoins.Remove(gc);
            }



            player.Update(Window, gameTime);


            
            //gå igenom alla finder
            foreach (Enemy e in enemies.ToList())
            {
                if (e.IsAlive)  //konrollera om fidnen lever
                {
                    //kontrollera kollision med spelaren
                    if (e.CheckCollision(player))
                        this.Exit();
                    e.Update(Window);  //flytta på dem
                }
                    
                else  //ta bort fienden om den är död
                    enemies.Remove(e);
            }
            //gå igenom alla fiender
            foreach( Enemy e in enemies.ToList())
            {
                //kontrollera om fienden kolliderar med skott
                foreach (Bullet b in player.Bullets)
                {
                    if(e.CheckCollision(b))
                    {
                        e.IsAlive = false;
                        player.Points++;
                    }
                }
                if (e.IsAlive) //kontrollera om fienden lever
                {
                    //kontrollera om kolliktion med spelaren
                    if (e.CheckCollision(player))
                        this.Exit();
                    e.Update(Window);

                }
                else //ta bort fienden för den är död
                    enemies.Remove(e);
            }





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);

            printText.Print("antal fiender " + enemies.Count, spriteBatch, 0, 0);
            
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            printText.Print("Points " + player.Points, spriteBatch, 0, 30);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
