﻿using System.Collections.Generic;

namespace hs_projekt_wzsi
{
    public class Player {

        public int lifePts; // punkty zycia gracza
        public int manaPts; // punkty many gracza
        public int mode;

        public List<Card> cardsInHand { get; set; } //karty w dloni gracza
        public List<Card> cardsOnTable { get; set; } //karty gracza na stole

        public Player (int lp, int mp)
        {
            lifePts = lp;
            manaPts = mp;
            cardsInHand = new List<Card>();
            cardsOnTable = new List<Card>();
        }


    };
}
