using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_projekt_wzsi
{
    public class GameState
    {

        public List<Card> cardsOnTable { get; set; } //karty gracza mcts na stole
        public List<Card> cardsInHand { get; set; } //karty gracza mcts na stole
        public List<Card> cardsOnTableMCTS { get; set; } //karty gracza na stole
        public List<Card> cardsInHandMCTS { get; set; } //karty gracza na stole
        public int  mctsHealth { get; set; }//zycie gracza mcts
        public int mctsMana { get; set; }//mana gracza mcts
        public int enemyHealth { get; set; }//zycie gracza
        public int enemyMana { get; set; }//mana gracza
 

        //zapisanie stanu gry
        public GameState(List<Card> mctsTable, List<Card> mctsHand, List<Card> enemyTable, List<Card> enemyHand, int ph, int pm, int ph1, int pm1)
        {
            //stan gracza MCTS
            cardsOnTableMCTS = mctsTable;
            cardsInHandMCTS = mctsHand;
            mctsHealth = ph;
            mctsMana = pm;

            //stan przeciwnika
            cardsOnTable = enemyTable;
            cardsInHand = enemyHand;
            enemyHealth = ph1;
            enemyMana = pm1;
        }

        //kopia stanu gry- gdy wezel zostanie wybrany 
        public void copyState(Player mcts, Player enemy)
        {
            //stan gracza MCTS
            mcts.cardsOnTable=cardsOnTableMCTS;
            mcts.cardsInHand=cardsInHandMCTS;
            mcts.lifePts=mctsHealth;
            mcts.manaPts=mctsMana;

            //stan przeciwnika
            enemy.cardsOnTable=cardsOnTable;
            enemy.cardsInHand=cardsInHand;
            enemy.lifePts=enemyHealth;
            enemy.manaPts=enemyMana;


        }


    }
}
