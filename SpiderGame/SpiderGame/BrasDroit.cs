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
    public class BrasDroit : Sprite
    {
        private float rotation;
        private Vector2 direction;
        private Kinect kinect;

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            position = new Vector2(graphics.PreferredBackBufferWidth / 2 + 26, graphics.PreferredBackBufferHeight / 2 + 116);
            kinect = Kinect.getInstance();
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            direction = kinect.MainDroite - position;
            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, 0), Vector2.One, SpriteEffects.None, 0);
        }

        public Vector2 getDirection()
        {
            return direction;
        }
    }
}
