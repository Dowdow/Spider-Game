using System;

namespace SpiderGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Point d’entrée principal pour l’application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Manager game = new Manager())
            {
                game.Run();
            }
        }
    }
#endif
}

