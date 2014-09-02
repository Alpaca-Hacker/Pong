using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Score
    {
       
        private readonly SpriteFont font;
        private readonly Rectangle gameBoundies;

        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }

        public Score(SpriteFont font, Rectangle gameBoundies)
        {
            this.font = font;
            this.gameBoundies = gameBoundies;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scoreText = String.Format("{0}:{1}", PlayerScore, ComputerScore);
            var xPosition = (gameBoundies.Width / 2 - font.MeasureString(scoreText).X / 2);
            var yPosition = 5;
            var location = new Vector2(xPosition, yPosition);

            spriteBatch.DrawString(font, scoreText, location, Color.Black);

            


        }

        internal void Update(GameTime gameTime, GameObjects gameObjects)
        {
            var ball = gameObjects.Ball;

            if (ball.Location.X < (0-ball.Width))
            {
                ComputerScore++;
                ball.Attachto(gameObjects.PlayerPaddle);
                gameObjects.Sounds.Play(Sound.Score);
            }

            if ((ball.Location.X+ball.Width) > (gameBoundies.Width))
            {
                PlayerScore++;
                ball.Attachto(gameObjects.PlayerPaddle);
                gameObjects.Sounds.Play(Sound.Score);
            }

           
        }
    }
}
