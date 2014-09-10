using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private Score score;
        private Sounds sounds;
        private SoundObjects soundObjects;
        private GameObjects gameObjects;
        private Rectangle gameBoundaries;
        private SpriteFont font;
        private bool isPaused;

        public const int WINPOINTS = 10;
        private bool Cheat = false;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            IsMouseVisible = true;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var playerTexture = (Content.Load<Texture2D>("PlayerPaddle"));
            var computerTexture = (Content.Load<Texture2D>("ComputerPaddle"));

            playerPaddle = new Paddle(playerTexture, Vector2.Zero, gameBoundaries, (Cheat ? Paddle.PlayerType.Cheat : Paddle.PlayerType.Human));
            computerPaddle = new Paddle(computerTexture, new Vector2((gameBoundaries.Width - computerTexture.Width), 0), gameBoundaries, (Cheat ? Paddle.PlayerType.Cheat : Paddle.PlayerType.Computer));

            ball = new Ball(Content.Load<Texture2D>("Ball"), Vector2.Zero, gameBoundaries);
            ball.Attachto(playerPaddle);
            font = Content.Load<SpriteFont>("SpriteFont1");
            score = new Score(font, gameBoundaries);

            var soundHit = (Content.Load<SoundEffect>("Blip"));
            var soundScore = (Content.Load<SoundEffect>("Powerup2"));
            var soundGameOver = (Content.Load<SoundEffect>("GameOver"));

            soundObjects = new SoundObjects
            {
                Hit = soundHit,
                Score = soundScore,
                GameOver = soundGameOver
            };
            sounds = new Sounds(soundObjects);
            gameObjects = new GameObjects
            {
                PlayerPaddle = playerPaddle,
                ComputerPaddle = computerPaddle,
                Ball = ball,
                Score = score,
                Sounds = sounds
            };


        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                {
                    ball.Attachto(playerPaddle);
                }
                playerPaddle.Update(gameTime, gameObjects);
                computerPaddle.Update(gameTime, gameObjects);
                ball.Update(gameTime, gameObjects);
                score.Update(gameTime, gameObjects);

            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    score.PlayerScore = 0;
                    score.ComputerScore = 0;
                    ball.Attachto(playerPaddle);
                    isPaused = false;
                }
            }
            base.Update(gameTime);
        }

        private void CheckWin()
        {
            if (score.PlayerScore == WINPOINTS || score.ComputerScore == WINPOINTS)
            {
                string winner = "COMPUTER";
                if (score.PlayerScore == WINPOINTS)
                {
                    winner = "PLAYER";
                }

                var gameOverText = "GAME OVER";
                var winnerText = String.Format("{0} WINS!", winner);
                var startText = "Press Enter to start";

                var gameOverPos = new Vector2((gameBoundaries.Width / 2 - font.MeasureString(gameOverText).X / 2), 100);
                var winnerPos = new Vector2((gameBoundaries.Width / 2 - font.MeasureString(winnerText).X / 2), 150);
                var startPos = new Vector2((gameBoundaries.Width / 2 - font.MeasureString(startText).X / 2), 300);


                spriteBatch.DrawString(font, gameOverText, gameOverPos, Color.DeepPink);
                spriteBatch.DrawString(font, winnerText, winnerPos, Color.BlueViolet);
                spriteBatch.DrawString(font, startText, startPos, Color.CornflowerBlue);

                if (!isPaused)
                {
                    sounds.Play(Sound.GameOver);
                    isPaused = true;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightCyan);

            spriteBatch.Begin();
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            score.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            CheckWin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
