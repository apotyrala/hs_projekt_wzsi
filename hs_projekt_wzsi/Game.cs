

using System;
using System.Collections.Generic;

namespace hs_projekt_wzsi
{

    public class Game
    {
        public List<Card> Deck1 = new List<Card>()
            {
            new Card { lifePts = 3, attackPts = 10, manaPts = 5 },
            new SpecialCard { lifePts = 4, attackPts = 10, manaPts = 5, damagePts =3 },
            new SpecialCard { lifePts = 5, attackPts = 10, manaPts = 5, healPts=1 },
            new Card { lifePts = 3, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 4, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 6, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 7, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 8, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 9, attackPts = 10, manaPts = 5 },

             };

        public List<Card> Deck2 = new List<Card>()
            {
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new SpecialCard { lifePts = 5, attackPts = 10, manaPts = 5, damagePts =3},
            new SpecialCard { lifePts = 5, attackPts = 10, manaPts = 5, healPts=1 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },

             };

       public List<Card> ShuffleList<Card>(List<Card> inputList)
        {
            List<Card> randomList = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

    }

}
