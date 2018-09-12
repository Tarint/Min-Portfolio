using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EttEgetSpel1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        SpriteFont gameFont;

        Texture2D myship;
        Texture2D coin;
        Texture2D tripod;
        Texture2D bullet;
        Texture2D life;
        Texture2D background_b;
        Texture2D background_e;
        Texture2D StartScreen;

        Vector2 myship_speed;
        Vector2 myship_pos;
        Vector2 coin_pos;
        Vector2 coin_speed;
        Vector2 tripod_pos;
        Vector2 tripod_speed;
        Vector2 bullet_pos;
        Vector2 life_pos;
        Vector2 life_speed;

        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        List<Vector2> bullet_pos_list = new List<Vector2>();
        List<Vector2> life_pos_list = new List<Vector2>();

        Rectangle rec_myship;
        Rectangle rec_coin;
        Rectangle rec_tripod;
        Rectangle rec_bullet;
        Rectangle rec_life;

        SoundEffect myshout;
        SoundEffect blaster;
        SoundEffect Explosion;
        SoundEffect background_m;

        bool hit = false;
        bool krash = false;
        bool shout = false;
        bool music;

        int timer = 0;
        int fire_blaster = 2;
        int HP;
        int poäng = 0;

        enum gameState
        {
            start,
            run,
            end,
            help
        }
        gameState gstate;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //this.graphics.IsFullScreen = true;
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

            myship_pos.X = 370;
            myship_pos.Y = 300;
            myship_speed.X = 5f;
            myship_speed.Y = 5f;

            coin_speed.Y = 7f;

            tripod_speed.Y = 10f;

            life_speed.Y = 7f;

            Random slump = new Random();
            for (int i = 0; i < 20; i++)
            {
                tripod_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                tripod_pos.Y = slump.Next(-200,0);
                tripod_pos_list.Add(tripod_pos);
            }
            for (int i = 0; i < 5; i++)
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coin_pos_list.Add(coin_pos);
            }
            for (int i = 0; i < 3; i++)
            {
                life_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                life_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                life_pos_list.Add(coin_pos);
            }
            HP = 3;

            gstate = gameState.start;

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

            // TODO: use this.Content to load your game content here
            myship = Content.Load<Texture2D>("Sprites/ship");
            coin = Content.Load<Texture2D>("Sprites/coin");
            tripod = Content.Load<Texture2D>("Sprites/tripod");
            bullet = Content.Load<Texture2D>("Sprites/bullet");
            life = Content.Load<Texture2D>("Sprites/Health");
            StartScreen = Content.Load<Texture2D>("Bilder/start");
            background_b = Content.Load<Texture2D>("Bilder/space");
            background_e = Content.Load<Texture2D>("Bilder/Game Over");
            myshout = Content.Load<SoundEffect>("Sounds/yehaw");
            blaster = Content.Load<SoundEffect>("Sounds/Star Wars Blaster sound effect");
            Explosion = Content.Load<SoundEffect>("Sounds/Explosion");
            background_m = Content.Load<SoundEffect>("Music/MW2");
            gameFont = Content.Load<SpriteFont>("Fonts/TEXT");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        // Funktion som kontrollerar kollision mellan 2 objekt
        public bool CheckCollision(Rectangle player, Rectangle mynt)
        {
            return player.Intersects(mynt);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (gstate == gameState.end && keyboardState.IsKeyDown(Keys.E))
            {
                this.Exit();
            }
            if (gstate == gameState.start && keyboardState.IsKeyDown(Keys.S))
            {
                gstate = gameState.run;
            }
            if (gstate == gameState.run)
            {
                //-----------------------------------------------------//
                if (keyboardState.IsKeyDown(Keys.D))
                    myship_pos.X = myship_pos.X + myship_speed.X;
                if (keyboardState.IsKeyDown(Keys.A))
                    myship_pos.X = myship_pos.X - myship_speed.X;
                if (keyboardState.IsKeyDown(Keys.S))
                    myship_pos.Y = myship_pos.Y + myship_speed.Y;
                if (keyboardState.IsKeyDown(Keys.W))
                    myship_pos.Y = myship_pos.Y - myship_speed.Y;
                if ((myship_pos.X) > Window.ClientBounds.Width - myship.Width)
                    myship_pos.X = Window.ClientBounds.Width - myship.Width;
                if (myship_pos.X < 0)
                    myship_pos.X = 0;
                if ((myship_pos.Y) > Window.ClientBounds.Height - myship.Height)
                    myship_pos.Y = Window.ClientBounds.Height - myship.Height;
                if (myship_pos.Y < 0)
                    myship_pos.Y = 0;
                rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X),
                    Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);
                foreach (Vector2 cn in coin_pos_list.ToList())
                {
                    rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X),
                        Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);
                    rec_coin = new Rectangle(Convert.ToInt32(cn.X),
                        Convert.ToInt32(cn.Y), coin.Width, coin.Height);
                    hit = CheckCollision(rec_myship, rec_coin);
                    if (hit == true)
                    {
                        coin_pos_list.Remove(cn);
                        hit = false;
                        poäng = poäng + 500;
                    }
                }
                rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X),
                Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);
                foreach (Vector2 enemy in tripod_pos_list.ToList())
                {
                    rec_tripod = new Rectangle(Convert.ToInt32(enemy.X),
                        Convert.ToInt32(enemy.Y), tripod.Width, tripod.Height);
                    krash = CheckCollision(rec_myship, rec_tripod);
                    if (krash == true)
                    {
                        Explosion.Play();
                        tripod_pos_list.Remove(enemy);
                        HP -= 1;
                        krash = false;
                    }
                }
                foreach (Vector2 cn in life_pos_list.ToList())
                {
                    rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X),
                        Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);
                    rec_life = new Rectangle(Convert.ToInt32(cn.X),
                        Convert.ToInt32(cn.Y), coin.Width, coin.Height);
                    hit = CheckCollision(rec_myship, rec_life);
                    if (hit == true)
                    {
                        life_pos_list.Remove(cn);
                        hit = false;
                        HP = HP + 1;
                    }
                }
                //-----------------------------------------------------//
                if (coin_pos_list.Count == 0)
                {
                    Random slump = new Random();
                    for (int i = 0; i < 5; i++)
                    {
                        coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                        coin_pos.Y = slump.Next(-200, 0);
                        coin_pos_list.Add(coin_pos);
                    }
                }
                for (int i = 0; coin_pos_list.Count > i; i++)
                {
                    coin_pos_list[i] = coin_pos_list[i] - new Vector2(0, -1);
                    if (coin_pos_list[i].Y > Window.ClientBounds.Height)
                        coin_pos_list.RemoveAt(i);
                }
                //-----------------------------------------------------//
                if (tripod_pos_list.Count == 0)
                {
                    Random slump = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        tripod_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                        tripod_pos.Y = slump.Next(-200, 0);
                        tripod_pos_list.Add(tripod_pos);
                    }
                }
                for (int i = 0; tripod_pos_list.Count > i; i++)
                {
                    tripod_pos_list[i] = tripod_pos_list[i] - new Vector2(0, -1);
                    if (tripod_pos_list[i].Y > Window.ClientBounds.Height)
                        tripod_pos_list.RemoveAt(i);
                }
                //-----------------------------------------------------//
                foreach (Vector2 bullets in bullet_pos_list.ToList())
                {
                    rec_bullet = new Rectangle(Convert.ToInt32(bullets.X),
                    Convert.ToInt32(bullets.Y), bullet.Width, bullet.Height);
                    foreach (Vector2 enemy in tripod_pos_list.ToList())
                    {
                        rec_tripod = new Rectangle(Convert.ToInt32(enemy.X), Convert.ToInt32(enemy.Y),
                            tripod.Width, tripod.Height);
                        hit = CheckCollision(rec_bullet, rec_tripod);
                        if (hit == true)
                        {
                            Explosion.Play();
                            tripod_pos_list.Remove(enemy);
                            bullet_pos_list.Remove(bullets);
                            hit = false;
                            poäng = poäng + 50;
                        }
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Space) && timer == 0)
                {
                    bullet_pos_list.Add(bullet_pos);
                    timer = 10;
                    switch (fire_blaster)
                    {
                        case 1:
                            bullet_pos = myship_pos;
                            fire_blaster++;
                            break;
                        case 2:
                            bullet_pos.X = myship_pos.X + 38;
                            bullet_pos.Y = myship_pos.Y + 0;
                            fire_blaster--;
                            break;
                    }
                }
                else if (timer > 0)
                {
                    timer--;
                }
                for (int i = 0; bullet_pos_list.Count > i; i++)
                {
                    bullet_pos_list[i] = bullet_pos_list[i] - new Vector2(0, 4);
                }
                //-----------------------------------------------------//
                if (life_pos_list.Count == 0)
                {
                    Random slump = new Random();
                    for (int i = 0; i < 1; i++)
                    {
                        life_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                        life_pos.Y = slump.Next(-200, 0);
                        life_pos_list.Add(life_pos);
                    }
                }
                for (int i = 0; life_pos_list.Count > i; i++)
                {
                    life_pos_list[i] = life_pos_list[i] - new Vector2(0, -1);
                    if (life_pos_list[i].Y > Window.ClientBounds.Height)
                        life_pos_list.RemoveAt(i);
                }
            }
            //-----------------------------------------------------//
            if (coin_pos_list.Count == 0 && shout == false)
            {
                myshout.Play();
                shout = true;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && timer == 0)
            {
                blaster.Play();
            }
            if (!music)
            {
                background_m.Play();
                music = true;
            }
            //-----------------------------------------------------//
            if (HP == 0)
            {
                gstate = gameState.end;
            }
            //-----------------------------------------------------//
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
            if (gstate == gameState.start)
            {
                spriteBatch.Draw(StartScreen, new Rectangle(0, 0,
                    graphics.PreferredBackBufferWidth,
                    graphics.PreferredBackBufferHeight),
                    Color.White);
            }
            if (HP >= 1)
            {
                if (gstate == gameState.run)
                {
                    spriteBatch.Draw(background_b, new Rectangle(0, 0,
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight),
                        Color.White);
                    spriteBatch.Draw(myship, myship_pos, Color.White);
                    foreach (Vector2 cn in coin_pos_list)
                    {
                        spriteBatch.Draw(coin, cn, Color.White);
                    }
                    foreach (Vector2 tripod_pos in tripod_pos_list)
                    {
                        spriteBatch.Draw(tripod, tripod_pos, Color.White);
                    }
                    foreach (Vector2 bullet_pos in bullet_pos_list)
                    {
                        spriteBatch.Draw(bullet, bullet_pos, Color.White);
                    }
                    foreach (Vector2 life_pos in life_pos_list)
                    {
                        spriteBatch.Draw(life, life_pos, Color.White);
                    }
                    //spriteBatch.Draw(explosion, explosion_pos, Color.White);
                    spriteBatch.DrawString(gameFont, "Points:" + poäng, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(gameFont, "HP:" + HP, new Vector2(10, 30), Color.White);
                }
            }
            if (gstate == gameState.end)
            {
                spriteBatch.Draw(background_e, new Rectangle(0, 0,
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight),
                        Color.White);
            }
                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
