using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Ball : Sprite
    {
        private Paddle attachedToPaddle;
        private Vector2 velocity = Vector2.Zero;

        public Ball(Texture2D texture, Vector2 location, Rectangle screenBounds) : base (texture,location, screenBounds)
        {

        }

        protected override void CheckBounds()
        {
            if (Location.Y >= (screenBounds.Height-texture.Height)  || Location.Y < 0)
            {
                velocity.Y = -(velocity.Y);
            }

        }

        public void Attachto(Paddle paddle)
        {
            attachedToPaddle = paddle;
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && attachedToPaddle != null)
            {
                var rnd = new Random();
                float y = (rnd.Next(100)/10f )-10f;
                velocity.X = +5f;
                velocity.Y = y;
                attachedToPaddle = null;
            }

            if (attachedToPaddle != null)
            {
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
                Location.Y = attachedToPaddle.Location.Y;
            }
            else
            {
                if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) || 
                    BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
                {
                    velocity.X = -velocity.X;
                }
            }
            delta = velocity;
            base.Update(gameTime, gameObjects);
        }
    }
}
