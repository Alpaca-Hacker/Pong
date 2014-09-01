using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Paddle : Sprite
    {

        public enum PlayerType
        {
            Human,
            Computer,
            Cheat
        }

        private PlayerType playerType;

        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds, PlayerType playerType) : base (texture, location, screenBounds)
        {
            this.playerType = playerType; 
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)

            
        {
            if (playerType == PlayerType.Computer)
            {
                var random = new Random();
                var reactions = random.Next(20, 40);

                Ball ball = gameObjects.Ball; 
                if ((ball.Location.Y-ball.Height) < Location.Y-reactions)
                {
                    delta = new Vector2(0, -4f);
                }

                if (ball.Location.Y > (Location.Y+this.Height-ball.Height)+reactions)
                {
                    delta = new Vector2(0, 4f);
                }
            }


            if (playerType == PlayerType.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    delta = new Vector2(0, -5f);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    delta = new Vector2(0, 5f);
                }
            }

            if (playerType == PlayerType.Cheat)
            {
                Location.Y = gameObjects.Ball.Location.Y;
            }


            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, (screenBounds.Height - texture.Height));

        }
    }
}
