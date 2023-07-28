using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameInvaders
{
    class Player
    {
        public Vector2 position;
        public Vector2 lastPosition;
        public Vector2 velocity;
        public Texture2D texture;

        public Player()
        {
            texture = Global.content.Load<Texture2D>("spr_ship");
            Reset();
        }

        public void Reset()
        {
            position.X = Global.width / 2; // horizontal center on screen
            position.Y = Global.height - texture.Height; // bottom of screen
        }

        public void Update()
        {
            // Assume player is not moving
            KeyboardState keyboard = Keyboard.GetState();
            lastPosition = position;
            velocity = Vector2.Zero;

            // Alter velocity when keys are pressed
            if (keyboard.IsKeyDown(Keys.Left))
            {
                velocity = new Vector2(-5, 0);
                position += velocity;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                velocity = new Vector2(5, 0);
                position += velocity;
            }
            // "clamp" the x-position to make sure it never goes out of screen bounds           
            if (position.X > Global.width - texture.Width || position.X < 0)
            {
                position = lastPosition;
            }

        }

        public void Draw()
        {
            Global.spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
