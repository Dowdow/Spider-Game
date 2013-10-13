using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpiderGame
{
    class PlayingState : GameState
    {
        /// <summary>
        /// SpriteFont pour le score et les combos
        /// </summary>
        SpriteFont scoreFont;

        // Instance de l'humain dans le jeu
        Human player;

        // Collection des projectiles instanciés dans le jeu
        List<Projectile> projectiles;

        // Collection des arraignées instanciées dans le jeu
        List<Spider> spiders;

        // Le score du joueur
        int score;

        // Multiplicateur de score
        int multiplicateur;

        // Le mode de difficulté du jeu
        int difficulty;

        // Permet de définir l'intervalle de temps pour lequel les arraignées doivent apparaître
        int spiderReady;

        // Permet de définir le temps actuel où le jeu se trouve par rapport à la prochaine apparition des arraignées
        int spiderLoad;

        // Permet de définir l'intervalle de temps pour lequel les projectiles doivent apparaître.
        int fireReady;

        // Permet de définir le temps actuel où le jeu se trouve par rapport à la prochaine apparition des projectiles
        int fireLoad;

        public PlayingState(GraphicsDeviceManager graphics, ContentManager content, int difficulty)
        {
            this.graphics = graphics;
            this.content = content;
            this.difficulty = difficulty;
        }

        /// <summary>
        /// Méthode qui va initialiser les composants du jeu
        /// </summary>
        public override void Initialize(Game game)
        {
            game.IsMouseVisible = false;

            player = new Human(graphics, content, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 18, graphics.PreferredBackBufferHeight / 2 + 80, 36, 148));

            projectiles = new List<Projectile>();
            spiders = new List<Spider>();

            background = new Background(game);
            background.Initialize();

            spiderReady = 100;
            fireReady = 25;

            fireLoad = 0;
            spiderLoad = 0;

            score = 0;
            multiplicateur = 1;
        }

        /// <summary>
        /// Méthode qui va charger les contenus du jeu
        /// </summary>
        public override void LoadContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            background.LoadContent("forest-background");
            scoreFont = content.Load<SpriteFont>("General");
        }

        /// <summary>
        /// Méthode qui va décharger les contenus du jeu
        /// </summary>
        public override void UnloadContent()
        {
            player.UnloadContent();
            foreach(Spider s in spiders)
            {
                s.UnloadContent();
            }
            foreach (Projectile p in projectiles)
            {
                p.UnloadContent();
            }
        }

        /// <summary>
        /// Méthode qui va mettre à jour les données du jeu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Kinect.getInstance().estDetecté())
            {

                // Mise à jour du joueur
                player.Update(gameTime);

                // Mise à jour des projectiles
                if (fireLoad == fireReady)
                {
                    projectiles.Add(new Projectile(graphics, content, new Rectangle(0, 0, 9, 9), player.brasDroit.getPosition(), player.brasDroit.getDirection()));
                    projectiles.Add(new Projectile(graphics, content, new Rectangle(0, 0, 9, 9), player.brasGauche.getPosition(), player.brasGauche.getDirection()));
                    fireLoad = 0;
                }
                fireLoad++;

                //Mise à jour des arraignées
                if (spiderLoad == spiderReady)
                {
                    spiders.Add(new Spider(graphics, content, new Rectangle(0, 0, 40, 36), difficulty));
                    spiderLoad = 0;
                }
                spiderLoad++;


                foreach (Projectile p in projectiles)
                {
                    p.Update(gameTime);
                    foreach (Spider s in spiders)
                    {
                        if (p.sourceRectangle.Intersects(s.sourceRectangle))
                        {
                            p.notExist = true;
                            s.lifePoint--;
                            break;
                        }
                    }
                }

                foreach (Spider s in spiders)
                {
                    s.Update(gameTime);

                    if (s.sourceRectangle.Intersects(player.sourceRectangle))
                    {
                        s.isDead = true;
                        player.lifePoint--;
                        player.life[player.lifePoint].Update(gameTime);
                        multiplicateur = 1;

                        if (player.lifePoint == 0)
                        {
                            OnGameStateChanged(new GameStateChangedEventArgs("GameOverState", score, difficulty));
                            break;
                        }
                    }
                }


                for (int i = projectiles.Count - 1; i >= 0; --i)
                {
                    if (projectiles[i].notExist)
                    {
                        projectiles[i].UnloadContent();
                        projectiles.RemoveAt(i);
                    }
                }

                for (int i = spiders.Count - 1; i >= 0; --i)
                {
                    if (spiders[i].isDead)
                    {
                        spiders[i].UnloadContent();
                        spiders.RemoveAt(i);
                        score += 5 * multiplicateur * difficulty;
                        multiplicateur++;
                    }
                }
            }
        }

        /// <summary>
        /// Méthode qui va dessiner les composants du jeu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            background.Draw(gameTime);
            
            foreach (Spider s in spiders)
            {
                s.Draw(spriteBatch);
            }

            foreach (Projectile p in projectiles)
            {
                p.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            Vector2 positionFont1 = new Vector2(graphics.PreferredBackBufferWidth-200, 10);
            String textScore = "Score : " + score.ToString();

            spriteBatch.DrawString(scoreFont, textScore, positionFont1, Color.White);

            Vector2 positionFont2 = new Vector2(graphics.PreferredBackBufferWidth - 200, 30);
            String textBonus = "Combo : " + (multiplicateur-1).ToString();

            spriteBatch.DrawString(scoreFont, textBonus, positionFont2, Color.White);
            
            if(!Kinect.getInstance().estDetecté())
                spriteBatch.DrawString(scoreFont, "Pause : En attente de detection" , new Vector2(250,200),Color.White);

            spriteBatch.End();
        }
    }
}
