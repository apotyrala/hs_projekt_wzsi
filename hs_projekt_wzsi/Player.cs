using System.Collections.Generic;

namespace hs_projekt_wzsi
{
    public class Player {

        public int lifePts; // punkty zycia gracza
        //int manaPts;

        public List<Card> cardsInHand; //karty w dloni gracza
        public List<Card> cardsOnTable; //karty gracza na stole

        public Player (int lp)
        {
            lifePts = lp;
        }


    };
}
