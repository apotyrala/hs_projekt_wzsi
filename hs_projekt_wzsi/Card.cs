using System.Collections.Generic;

namespace hs_projekt_wzsi
{
    public class Card {

        //int lifePts; //zycie karty
        //int attackPts; // punkty ataku (odejmowane od zycia przeciwnika)
        //int manaPts; // koszt karty

       /* public Card(int lifePts, int attackPts, int manaPts)
        {
            this.lifePts = lifePts;
            this.attackPts = attackPts;
            this.manaPts = manaPts;
        }*/

        public int lifePts 
        {
            get; set;
        }

        public int attackPts
        {
            get; set;
        }

        public int manaPts
        {
            get; set;
        }



        //public int healPts
        //{
        //    get; set;
        //}
        //public int damagePts
        //{
        //    get; set;
        //}
    };
}
