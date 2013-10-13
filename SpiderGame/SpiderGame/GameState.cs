using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpiderGame
{
    abstract class GameState
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected ContentManager content;
        protected Background background;

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        public abstract void Initialize(Game game);

        public abstract void LoadContent(SpriteBatch spriteBatch);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        protected virtual void OnGameStateChanged(GameStateChangedEventArgs args)
        {
            if (GameStateChanged != null)
            {
                GameStateChanged(this, args);
            }
        }
    }
}
