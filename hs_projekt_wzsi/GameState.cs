using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_projekt_wzsi
{
    public class GameState
    {

        public List<Card> cardsOnTable { get; set; } //karty gracza na stole
        public List<Card> cardsInHand { get; set; } //karty gracza na stole
        public int  playerHealth { get; set; }//zycie gracza
        public int playerMana { get; set; }//mana gracza
        public Player player;
 
        //zapisanie stanu gry, tj interesuje nas stan Gracza MCTS
        public GameState(List<Card> cardsOnTable, List<Card> cardsInHand, Player p, int ph, int pm)
        {
            p.cardsOnTable = cardsOnTable;
            p.cardsInHand = cardsInHand;
            playerHealth = ph;
            playerMana = pm;
        }


    }
}
