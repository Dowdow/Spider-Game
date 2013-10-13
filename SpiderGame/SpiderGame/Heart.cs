using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpiderGame
{
    public class Heart : Sprite
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        // La position du sprite sur la spritesheet
        private Rectangle positionSprite;

        private int index;

        public Heart(GraphicsDeviceManager graphics, ContentManager content, int index)
        {
            this.graphics = graphics;
            this.content = content;
            this.index = index;
            positionSprite = new Rectangle(0, 0, 26, 25);

            Initialize(graphics);
            LoadContent(content, "heart");
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            position = new Vector2(26 * index + 5, 5); 
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
            positionSprite = new Rectangle(26, 0, 26, 25);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, positionSprite, Color.White);
        }
    }
}
