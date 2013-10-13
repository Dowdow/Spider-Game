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
    public class Human : Sprite
    {
        public BrasGauche brasGauche;
        public BrasDroit brasDroit;

        public List<Heart> life;

        // Les points de vie de l'humain
        public int lifePoint;

        // Rectangle définissant la hitbox de l'humain
        public Rectangle sourceRectangle;

        /// <summary>
        /// Constructeur du Human
        /// </summary>
        /// <param name="position">position de base du Human</param>
        public Human(GraphicsDeviceManager graphics, ContentManager content, Rectangle sourceRectangle)
        {
            lifePoint = 3;
            life = new List<Heart>();

            for(int i=0; i<lifePoint; i++)
            {
                life.Add(new Heart(graphics, content, i));
            }
            this.sourceRectangle = sourceRectangle;
            brasGauche = new BrasGauche();
            brasDroit = new BrasDroit();
            Initialize(graphics);
            LoadContent(content, "human");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            position = new Vector2(graphics.PreferredBackBufferWidth / 2 - 18, graphics.PreferredBackBufferHeight / 2 + 80);
            brasGauche.Initialize(graphics);
            brasDroit.Initialize(graphics);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            brasGauche.LoadContent(content, "brasGauche");
            brasDroit.LoadContent(content, "brasDroit");
        }

        public override void UnloadContent()
        {
            brasGauche.UnloadContent();
            brasDroit.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Heart h in life)
            {
                h.UnloadContent();
            }
            brasGauche.Update(gameTime);
            brasDroit.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach(Heart h in life)
            {
                h.Draw(spriteBatch);
            }
            brasGauche.Draw(spriteBatch);
            brasDroit.Draw(spriteBatch);
        }
    }
}
