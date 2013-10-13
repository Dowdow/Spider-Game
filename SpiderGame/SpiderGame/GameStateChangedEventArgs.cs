using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderGame
{
    class GameStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Le nom de l'état
        /// </summary>
        public string name
        {
            get;
            private set;
        }

        /// <summary>
        /// Le score du joueur
        /// </summary>
        public int score
        {
            get;
            private set;
        }

        /// <summary>
        /// La difficulté sélectionnée
        /// </summary>
        public int difficulty {
            get;
            set;
        }

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="new_info">nouvelle info</param>
        /// <param name="info_date">date de parution de l'info</param>
        public GameStateChangedEventArgs(string name, int? score, int? difficulty)
        {
            this.name = name;

            if (score != null)
            {
                this.score = (int)score;
            }

            if (difficulty != null)
            {
                this.difficulty = (int)difficulty;
            }
        }
    }
}
