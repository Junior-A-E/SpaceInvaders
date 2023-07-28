using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameInvaders;

namespace SpaceInvaders
{
    class Invader
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D texture;
        public float speed = 1f;
        public int order;
        public int color;

        public Invader(int col, int pos)
        {
            order = pos;
            color = col;
            if (color == 0)
            {
                texture = Global.content.Load<Texture2D>("spr_green_invader");
            }
            else if (color == 1)
            {
                texture = Global.content.Load<Texture2D>("spr_blue_invader");
            }
            else if (color == 2)
            {
                texture = Global.content.Load<Texture2D>("spr_red_invader");
            }
            Reset();
        }

        public void Reset()
        {
            position.X = order * texture.Width;
            position.Y = color * texture.Height;
        }

        public void Update()
        {
            position.X += speed;

            if (position.X > Global.width - texture.Width)
            {
                position.X = Global.width - texture.Width;
                speed *= -1f;
                position.Y += 12;
            }
            if (position.X < 0)
            {
                position.X = 0;
                speed *= -1f;
                position.Y += 12;
            }
        }

        public void Draw()
        {
            Global.spriteBatch.Draw(texture, position, Color.White);
        }
    }

}