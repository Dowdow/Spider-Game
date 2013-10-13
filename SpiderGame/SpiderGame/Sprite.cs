using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpiderGame
{
    public abstract class Sprite
    {
        protected Vector2 position;

        protected Texture2D texture;

        public abstract void Initialize(GraphicsDeviceManager graphics);

        public abstract void LoadContent(ContentManager content, String assetName);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public Vector2 getPosition()
        {
            return position;
        }

        public Texture2D getTexture()
        {
            return texture;
        }
    }
}
