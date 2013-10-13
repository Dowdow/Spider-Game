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
    public class Spider : Sprite
    {
        private Vector2 destination;

        // L'index actuel de la sprite sheet
        float index;

        // L'index maximum que l'on peut atteindre dans la sprite sheet
        int maxIndex;

        // Le type de déplacement que l'arraignée doit avoir (0 pour vers le bas, 1 pour vers la gauche, 2 pour vers la droite)
        int typeDeplacement;

        // Le nombre de points de vie de l'arraignée
        public int lifePoint;

        // Définit si l'arraignée est morte ou non
        public bool isDead;
        
        private GraphicsDeviceManager graphics;

        // La position de l'humain sur l'écran
        private Vector2 humanPosition;

        // Rectangle définissant la hitbox de l'arraignée
        public Rectangle sourceRectangle;

        // Rectangle définissant le sprite à afficher sur la stylesheet "spider"
        public Rectangle positionSprite;

        // Le multiplicateur de vitesse de l'arraignée
        public float multiplicateurVitesse;

        /// <summary>
        /// Constructeur du Spider
        /// </summary>
        /// <param name="position">position de base du Spider</param>
        public Spider(GraphicsDeviceManager graphics, ContentManager content, Rectangle sourceRectangle, int difficulty)
        {
            Random r = new Random();
            lifePoint = 4;
            index = 0;
            try
            {
                position = new Vector2(r.Next(graphics.PreferredBackBufferWidth)%(graphics.PreferredBackBufferWidth-40), 0);
            }
            catch(DivideByZeroException e)
            { 
                Console.WriteLine(e.Message);
            }

            this.sourceRectangle = sourceRectangle;
            this.positionSprite = sourceRectangle;

            destination = Vector2.Zero;

            maxIndex = 3;

            typeDeplacement = 0;

            humanPosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - 18, graphics.PreferredBackBufferHeight / 2 + 180);

            switch (difficulty)
            {
                case 1: this.multiplicateurVitesse = 0.5f;
                        break;
                case 2: this.multiplicateurVitesse = 1;
                        break;
                case 3: this.multiplicateurVitesse = 1.5f;
                        break;
                case 4: this.multiplicateurVitesse = 2;
                        break;
                default: this.multiplicateurVitesse = 2.5f;
                        break;
            }


            Initialize(graphics);
            LoadContent(content, "spider");
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
            if (maxIndex != 0)
            {
                index += 0.1f;

                if (index > maxIndex)
                {
                    index = 0 + typeDeplacement * 3;
                }

                positionSprite = new Rectangle((int)index * 40, 0, sourceRectangle.Width, sourceRectangle.Height);
            }

            if(lifePoint <= 0)
            {
                isDead = true;
            }

            else
            {
                if (position.Y < humanPosition.Y)
                {
                    position.Y += 1*multiplicateurVitesse;
                    sourceRectangle.X = (int)position.X;
                    sourceRectangle.Y = (int)position.Y;
                }
                else
                {
                    if (destination == Vector2.Zero)
                    {
                        destination = humanPosition - position;
                        destination.Normalize();

                        if(destination.X < 0)
                        {
                            typeDeplacement = 1;
                            index = 3;
                            maxIndex = 6;
                        }
                        else
                        {
                            typeDeplacement = 2;
                            index = 6;
                            maxIndex = 9;
                        }
                    }

                    position += destination*multiplicateurVitesse;
                    sourceRectangle.X = (int)position.X;
                    sourceRectangle.Y = (int)position.Y;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, positionSprite, Color.White);
        }
    }
}
