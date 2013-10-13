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
    class MainMenuState : GameState
    {
        // Les différentes textures du menu principal
        Texture2D startButton;

        Texture2D exitButton;

        Texture2D cancelButton;

        Texture2D easyButton;

        Texture2D normalButton;

        Texture2D mediumButton;

        Texture2D hardButton;

        Texture2D godButton;

        Texture2D title;

        // Les différentes positions des textures du menu principal
        Vector2 startButtonPosition;

        Vector2 exitButtonPosition;

        Vector2 cancelButtonPosition;

        Vector2 easyButtonPosition;

        Vector2 normalButtonPosition;

        Vector2 mediumButtonPosition;

        Vector2 hardButtonPosition;

        Vector2 godButtonPosition;

        Vector2 titlePosition;

        // Rectangle définissant la position de chaque bouton
        Rectangle startButtonRect;

        Rectangle exitButtonRect;

        Rectangle easyButtonRect;

        Rectangle normalButtonRect;

        Rectangle mediumButtonRect;

        Rectangle hardButtonRect;

        Rectangle godButtonRect;

        Rectangle cancelButtonRect;

        // Détermine si le joueur est sur l'écran de choix des difficultés ou non
        bool playButtonChoosed;

        // Etat précédent de la souris
        MouseState previousState;

        // Instance du jeu
        Game game;

        public MainMenuState(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.graphics = graphics;
            this.content = content;
        }

        public override void Initialize(Game game)
        {
            background = new Background(game);
            background.Initialize();

            this.game = game;

            game.IsMouseVisible = true;

            titlePosition = new Vector2(graphics.PreferredBackBufferWidth / 4.5f, graphics.PreferredBackBufferHeight / 5);

            startButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.3f);
            exitButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.3f + 25);

            easyButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f);
            normalButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f + 25);
            mediumButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f + 50);
            hardButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f + 75);
            godButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f + 100);
            cancelButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2.4f, graphics.PreferredBackBufferHeight / 1.5f + 125);

            startButtonRect = new Rectangle((int)startButtonPosition.X,
                                            (int)startButtonPosition.Y, 150, 25);
            exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                     (int)exitButtonPosition.Y, 150, 25);

            easyButtonRect = new Rectangle((int)easyButtonPosition.X,
                            (int)easyButtonPosition.Y, 150, 25);
            normalButtonRect = new Rectangle((int)normalButtonPosition.X,
                                         (int)normalButtonPosition.Y, 150, 25);
            mediumButtonRect = new Rectangle((int)mediumButtonPosition.X,
                                        (int)mediumButtonPosition.Y, 150, 25);
            hardButtonRect = new Rectangle((int)hardButtonPosition.X,
                                         (int)hardButtonPosition.Y, 150, 25);
            godButtonRect = new Rectangle((int)godButtonPosition.X,
                                        (int)godButtonPosition.Y, 150, 25);
            cancelButtonRect = new Rectangle((int)cancelButtonPosition.X,
                                         (int)cancelButtonPosition.Y, 150, 25);

        }

        public override void LoadContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            background.LoadContent("mainMenuScreen");
            title = content.Load<Texture2D>("title");
            startButton = content.Load<Texture2D>("start");
            exitButton = content.Load<Texture2D>("exit");
            easyButton = content.Load<Texture2D>("easy");
            normalButton = content.Load<Texture2D>("normal");
            mediumButton = content.Load<Texture2D>("medium");
            hardButton = content.Load<Texture2D>("hard");
            godButton = content.Load<Texture2D>("god");
            cancelButton = content.Load<Texture2D>("cancel");
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

            if (!playButtonChoosed)
            {
                if (mouseClickRect.Intersects(startButtonRect))
                {
                    playButtonChoosed = true;
                }
                else if (mouseClickRect.Intersects(exitButtonRect))
                {
                    Kinect.getInstance().KinectStop();
                    game.Exit();
                }
            }

            else
            {
                if (mouseClickRect.Intersects(easyButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, 1));
                }
                if (mouseClickRect.Intersects(normalButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, 2));
                }
                if (mouseClickRect.Intersects(mediumButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, 3));
                }
                if (mouseClickRect.Intersects(hardButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, 4));
                }
                if (mouseClickRect.Intersects(godButtonRect))
                {
                    OnGameStateChanged(new GameStateChangedEventArgs("PlayingState", 0, 5));
                }
                if (mouseClickRect.Intersects(cancelButtonRect))
                {
                    playButtonChoosed = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            background.Draw(gameTime);
            spriteBatch.Draw(title, titlePosition, Color.White);

            if (!playButtonChoosed)
            {
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            }

            else
            {
                spriteBatch.Draw(easyButton, easyButtonPosition, Color.White);
                spriteBatch.Draw(normalButton, normalButtonPosition, Color.White);
                spriteBatch.Draw(mediumButton, mediumButtonPosition, Color.White);
                spriteBatch.Draw(hardButton, hardButtonPosition, Color.White);
                spriteBatch.Draw(godButton, godButtonPosition, Color.White);
                spriteBatch.Draw(cancelButton, cancelButtonPosition, Color.White);
            }

            spriteBatch.End();
        }
    }
}
