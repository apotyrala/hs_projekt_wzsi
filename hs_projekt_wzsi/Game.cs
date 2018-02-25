

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
                randomIndex = r.Next(0, inputList.Count); // wybierz randomowy obiekt z losty
                randomList.Add(inputList[randomIndex]); //dodaj go do nowej, randomowej listy
                inputList.RemoveAt(randomIndex); //usun by uniknac duplikatow
            }

            return randomList; //zwroc nowa liste
        }

        public void GamePlay() // rozgrywka
        {
            Player player1 = new Player(20);
            Player player2 = new Player(20);

            //tasowanie kart
            List<Card> shuffledDeck1 = new List<Card>();
            shuffledDeck1 = ShuffleList(Deck1);

            List<Card> shuffledDeck2 = new List<Card>();
            shuffledDeck2 = ShuffleList(Deck2);

            //rozdanie kart- gracz 1 zaczyna z czterema kartami, gracz 2 z trzema

            for (int i = 0; i < 4; i++)
            {
                player1.cardsInHand[i] = shuffledDeck1[i];
                shuffledDeck1.RemoveAt(i); //usuniecie pobranych kart
            }

            for (int i = 0; i < 3; i++)
            {
                player2.cardsInHand[i] = shuffledDeck2[i];
                shuffledDeck2.RemoveAt(i);
            }

            //pierwsza tura- gracze rzucaja karty na stol (dla ulatwienia pierwsza z listy)
            player1.cardsOnTable[0] = player1.cardsInHand[0];
            player1.cardsInHand.RemoveAt(0);//karta usunieta z reki

            player2.cardsOnTable[0] = player2.cardsInHand[0];
            player2.cardsInHand.RemoveAt(0);

            //dobieranie karty(dla ulatwienia pierwsza z listy)
            player1.cardsInHand.Add(shuffledDeck1[0]);
            shuffledDeck1.RemoveAt(0);//karta usunieta z puli

            player2.cardsInHand.Add(shuffledDeck2[0]);
            shuffledDeck2.RemoveAt(0);
        }

    }

}
