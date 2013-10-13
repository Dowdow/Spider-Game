using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpiderGame
{
    class GameOverState : GameState
    {
        // Les différentes textures liées à l'écran de fin de jeu
        Texture2D retryButton;
        Texture2D mainMenuButton;
        Texture2D exitButton;

        // La position des différents boutons
        Vector2 retryButtonPosition;
        Vector2 mainMenuButtonPosition;
        Vector2 exitButtonPosition;

        // Rectangle définissant la position de chaque bouton
        Rectangle retryButtonRect;
        Rectangle mainMenuButtonRect;
        Rectangle exitButtonRect;

        //Position de la font du score
        Vector2 positionFont;

        //Texte d'affichage du score
        String textScore;

        // Le score final du joueur à la fin de la partie
        public int finalScore;

        // La difficulté du jeu
        public int difficulty;

        // Etat précédent de la souris
        MouseState previousState;

        /// <summary>
        /// SpriteFont pour le score
        /// </summary>
        SpriteFont scoreFont;

        //Instance du jeu
        Game game;

        public GameOverState(GraphicsDeviceManager graphics, ContentManager content, int score, int difficulty)
        {
            this.graphics = graphics;
            this.content = content;
            finalScore = score;
            this.difficulty = difficulty;
        }

        public override void Initialize(Game game)
        {
            game.IsMouseVisible = true;

            background = new Background(game);
            background.Initialize();
            this.game = game;

            retryButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 1.25f);
            mainMenuButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 1.25f + 25);
            exitButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 1.25f + 50);

            retryButtonRect = new Rectangle((int)retryButtonPosition.X,
                            (int)retryButtonPosition.Y, 150, 25);
            mainMenuButtonRect = new Rectangle((int)mainMenuButtonPosition.X,
                                         (int)mainMenuButtonPosition.Y, 150, 25);
            exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                        (int)exitButtonPosition.Y, 150, 25);

            positionFont = new Vector2(graphics.PreferredBackBufferWidth / 2.7f, graphics.PreferredBackBufferHeight / 1.5f);
            textScore = "Player's score : " + finalScore.ToString();
        }

        public override void LoadContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            background.LoadContent("gameOverScreen");
            scoreFont = content.Load<SpriteFont>("General");

            retryButton = content.Load<Texture2D>("retry");
            mainMenuButton = content.Load<Texture2D>("mainMenu");
            exitButton = content.Load<Texture2D>("exit");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (previousState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousState = mouseState;
        }

        void MouseClicked(int x, int y)
        {
            Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

                if (mouseClickRect.Intersects(retryButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, difficulty));
                }
                if (mouseClickRect.Intersects(mainMenuButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("MainMenuState", 0, 0));
                }
                if (mouseClickRect.Intersects(exitButtonRect))
                {
                    Kinect.getInstance().KinectStop();
                    game.Exit();
                }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            background.Draw(gameTime);

            spriteBatch.DrawString(scoreFont, textScore, positionFont, Color.White);
            spriteBatch.Draw(retryButton, retryButtonPosition, Color.White);
            spriteBatch.Draw(mainMenuButton, mainMenuButtonPosition, Color.White);
            spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);

            spriteBatch.End();
        }
    }
}
