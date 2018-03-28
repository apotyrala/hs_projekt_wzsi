using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hs_projekt_wzsi
{
    static class Program
    {
        
    
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TODO: menu
            Game game = new Game();
            int pl1, pl2;
            Console.WriteLine("Wybierz tryb gracza 1 (1 - losowy, 2 - agresywny, 3 - kontrolujacy, 4-MCTS)");
            pl1 = Int32.Parse(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine("\nWybierz tryb gracza 2 (1 - losowy, 2 - agresywny, 3 - kontrolujacy, 4-MCTS)");
            pl2 = Int32.Parse(Console.ReadKey().KeyChar.ToString());
            game.GamePlay(pl1, pl2);

        }


    }
}
