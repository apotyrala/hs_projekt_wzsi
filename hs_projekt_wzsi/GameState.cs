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


        public List<Card> sd1 { get; set; } //karty gracza w decku 1
        public List<Card> sd2 { get; set; } //karty gracza w decku 2

        //zapisanie stanu gry
        public GameState(List<Card> mctsTable, List<Card> mctsHand, List<Card> enemyTable, List<Card> enemyHand, int ph, int pm, int ph1, int pm1, List<Card> shuffled1, List<Card> shuffled2)
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

            sd1 = shuffled1;
            sd2 = shuffled2;
        }

        //kopia stanu gry- gdy wezel zostanie wybrany 
        public void copyState(Player mcts, Player enemy, GameState gs, List<Card> shuffled1, List<Card> shuffled2)
        {
            //stan gracza MCTS
            mcts.cardsOnTable= gs.cardsOnTableMCTS;
            mcts.cardsInHand= gs.cardsInHandMCTS;
            mcts.lifePts= gs.mctsHealth;
            mcts.manaPts= gs.mctsMana;

            //stan przeciwnika
            enemy.cardsOnTable= gs.cardsOnTable;
            enemy.cardsInHand= gs.cardsInHand;
            enemy.lifePts= gs.enemyHealth;
            enemy.manaPts= gs.enemyMana;

            shuffled1 = gs.sd1;
            shuffled2 = gs.sd2;


        }


    }
}
