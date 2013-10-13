using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpiderGame
{
    public class Manager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        // Etat actuel du jeu : Menu Principal, Jeu ou Game Over
        GameState currentState;

        /// <summary>
        /// Constructeur complet
        /// </summary>
        public Manager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Spider Nightmare";
            currentState = new MainMenuState(graphics, Content);
            currentState.GameStateChanged += new EventHandler<GameStateChangedEventArgs>(GameStateChanged);
            Kinect.getInstance();
        }

        /// <summary>
        /// Méthode lancée à la réception d'une demande de changement d'état du jeu
        /// </summary>
        /// <param name="sender">L'objet envoyant l'information</param>
        /// <param name="e">Les arguments de l'évènement</param>
        void GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            if (e.name.Equals("MainMenuState"))
            {
                currentState = new MainMenuState(graphics, Content);
            }
            if (e.name.Equals("PlayingState"))
            {
                currentState = new PlayingState(graphics, Content, e.difficulty);
            }
            if (e.name.Equals("GameOverState"))
            {
                currentState = new GameOverState(graphics, Content, e.score, e.difficulty);
            }

            currentState.GameStateChanged += new EventHandler<GameStateChangedEventArgs>(GameStateChanged);
            currentState.Initialize(this);
            currentState.LoadContent(spriteBatch);
        }

        /// <summary>
        /// Méthode qui va initialiser les composants du jeu
        /// </summary>
        protected override void Initialize()
        {
            currentState.Initialize(this);

            base.Initialize();
        }

        /// <summary>
        /// Méthode qui va charger les contenus du jeu
        /// </summary>
        protected override void LoadContent()
        {
            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState.LoadContent(spriteBatch);
        }

        /// <summary>
        /// Méthode qui va décharger les contenus du jeu
        /// </summary>
        protected override void UnloadContent()
        {
            currentState.UnloadContent();
        }

        /// <summary>
        /// Méthode qui va mettre à jour les données du jeu
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // Permet au jeu de se fermer
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Kinect.getInstance().KinectStop();
                this.Exit();
            }
            
            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Méthode qui va dessiner les composants du jeu
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
