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
    public class Projectile : Sprite
    {
        private Vector2 destination;
        private float vitesse = 5;
        private GraphicsDeviceManager graphics;
        public bool notExist;

        // Rectangle définissant la hitbox du projectile
        public Rectangle sourceRectangle;

        /// <summary>
        /// Constructeur du Projectile
        /// </summary>
        /// <param name="position">position de base du Projectile</param>
        public Projectile(GraphicsDeviceManager graphics, ContentManager content, Rectangle sourceRectangle, Vector2 source, Vector2 destination)
        {
            notExist = false;
            this.position = source;
            this.destination = destination;
            this.destination.Normalize();
            this.sourceRectangle = sourceRectangle;
            Initialize(graphics);
            LoadContent(content, "projectile");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
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
            position += destination * vitesse;
            sourceRectangle.X = (int)position.X;
            sourceRectangle.Y = (int)position.Y;

            if (getPosition().X <= 0  || getPosition().Y <= 0 || getPosition().X >= graphics.PreferredBackBufferWidth || getPosition().Y >= graphics.PreferredBackBufferHeight)
            {
                notExist = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
