using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGameInvaders
{
    class Bullet
    {
        public Boolean isFired = false;
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D texture;
        public float speed;

        public Bullet()
        {
            texture = Global.content.Load<Texture2D>("spr_bullet");
            speed = 3f;
            Reset();
        }

        public void Reset()
        {
            position = Vector2.Zero; // horizontal center on screen
            velocity = Vector2.Zero; // bottom of screen
            isFired = false;
        }

        public void Update()
        {
            if (isFired)
            {
                position += velocity * speed;
                if (position.Y < 0 || position.Y > Global.height)
                {
                    Reset();
                }
            }
        }

        public void Draw()
        {
            if (isFired)
            {
                Global.spriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Fire(Vector2 startPosition)
        {
            if (!isFired)
            {
                position.X = startPosition.X;
                position.Y = startPosition.Y - 20;
                velocity = new Vector2(0, -1);
                isFired = true;
            }
        }

    }
}
