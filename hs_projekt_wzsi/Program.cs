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
            Game game = new Game();
            game.GamePlay();

        }


    }
}
