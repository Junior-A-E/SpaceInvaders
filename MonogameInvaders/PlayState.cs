using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using System.Collections.Generic;

namespace MonoGameInvaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PlayState : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, scanlines;

        Player thePlayer;
        List<Invader> invaders;
        List<Bullet> bullets;

        bool keyReleased = true;
        private SpriteFont font;
        private int score = 0;

        private bool gameOver = false;

        //TODO: Add multiple invaders here

        public PlayState()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

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
            // Pass often referenced variables to Global
            Global.GraphicsDevice = GraphicsDevice;
            Global.content = Content;

            // Create and Initialize game objects
            thePlayer = new Player();
            invaders = new List<Invader>();
            bullets = new List<Bullet>();

            for (int i = 0; i < 9; i++)
            {
                invaders.Add(new Invader(0, i));
            }
            for (int i = 0; i < 9; i++)
            {
                invaders.Add(new Invader(1, i));
            }
            for (int i = 0; i < 9; i++)
            {
                invaders.Add(new Invader(2, i));
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.spriteBatch = spriteBatch;
            background = Content.Load<Texture2D>("spr_background");
            scanlines = Content.Load<Texture2D>("spr_scanlines");
            font = Content.Load<SpriteFont>("scoreFont");
            base.Initialize();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (score >= 27)
            {
                gameOver = true;
                return;
            }
            // Pass keyboard state to Global so we can use it everywhere
            Global.keys = Keyboard.GetState();
            if (Global.keys.IsKeyDown(Keys.Space) && keyReleased)
            {
                Bullet newBullet = new Bullet();
                newBullet.Fire(thePlayer.position);
                bullets.Add(newBullet);
                keyReleased = false;
            }
            if (Global.keys.IsKeyUp(Keys.Space))
            {
                keyReleased = true;
            }

            // Update the game objects
            thePlayer.Update();

            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
            }
            foreach (Invader invader in invaders)
            {
                invader.Update();
            }

            List<Invader> invaderEliminated = new List<Invader>();
            List<Bullet> bulletTouched = new List<Bullet>();

            foreach (Bullet bullet in bullets)
            {
                //bullet.Update();
                foreach (Invader invader in invaders)
                {
                    //invader.Update();
                    Rectangle bulletRect = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bullet.texture.Width, bullet.texture.Height);
                    Rectangle invaderRect = new Rectangle((int)invader.position.X, (int)invader.position.Y, invader.texture.Width, invader.texture.Height);

                    if (bulletRect.Intersects(invaderRect))
                    {
                        score++;
                        invaderEliminated.Add(invader);
                        bulletTouched.Add(bullet);
                    }
                }
            }

            foreach (Bullet bullet in bulletTouched)
            {
                bullets.Remove(bullet);
            }
            bulletTouched.Clear();
            foreach (Invader invader in invaderEliminated)
            {
                invaders.Remove(invader);
            }
            invaderEliminated.Clear();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameOver)
            {
                //GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();

                Vector2 textPosition = new Vector2(Global.width / 2, Global.width / 2);
                spriteBatch.DrawString(font, $"You Won :) ", textPosition, Color.White);

                spriteBatch.End();
                return;
            }

            spriteBatch.Begin();
            // Draw the background (and clear the screen)
            spriteBatch.Draw(background, Global.screenRect, Color.White);

            // Draw the game objects
            thePlayer.Draw();

            foreach (Invader invader in invaders)
            {
                invader.Draw();
            }

            foreach (Bullet bullet in bullets)
            {

                bullet.Draw();
            }

            spriteBatch.Draw(scanlines, Global.screenRect, Color.White);
            spriteBatch.DrawString(font, "Score : " + score.ToString(), new Vector2(10, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
