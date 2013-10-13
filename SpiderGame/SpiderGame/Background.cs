using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpiderGame
{
    class Background : DrawableGameComponent
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected SpriteBatch spriteBatch;

        public Background(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            position = new Vector2(0, 0);
            base.Initialize();
        }

        public void LoadContent(String assetName)
        {
            texture = Game.Content.Load<Texture2D>(assetName);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
