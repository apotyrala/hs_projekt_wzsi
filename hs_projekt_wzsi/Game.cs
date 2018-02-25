

using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            //pierwsza tura- gracze rzucaja karty na stol (dla ulatwienia pierwsza z listy)- nie mozna jej uzyc w tej samej turze
            player1.cardsOnTable[0] = player1.cardsInHand[0];
            player1.cardsInHand.RemoveAt(0);//karta usunieta z reki

            player2.cardsOnTable[0] = player2.cardsInHand[0];
            player2.cardsInHand.RemoveAt(0);


            while (player1.lifePts >= 0 && player2.lifePts >= 0)
            {
                int k = 1; //zmienna do odejmowanie punktow zycia gracza

                if (shuffledDeck1.Count != 0)  //jezeli mozna jeszcze pobrac karty
                {
                    //dobieranie karty(dla ulatwienia pierwsza z listy)
                    player1.cardsInHand.Add(shuffledDeck1[0]);
                    shuffledDeck1.RemoveAt(0);//karta usunieta z puli
                    player1.lifePts = player1.lifePts - k;//odjecie punktow zycia graczowi za pobranie karty- po kazdej turze o 1 wiecej
                }

                if (shuffledDeck2.Count != 0)
                {
                    player2.cardsInHand.Add(shuffledDeck2[0]);
                    shuffledDeck2.RemoveAt(0);
                    player2.lifePts = player2.lifePts - k;
                }

                k = k + 1;

                //druga i kolejne tury- gracze rzucaja karte, nie moga jej uzyc (tylko te z poprzednich tur)
                player1.cardsOnTable[0] = player1.cardsInHand[0];
                player1.cardsInHand.RemoveAt(0);//karta usunieta z reki

                player2.cardsOnTable[0] = player2.cardsInHand[0];
                player2.cardsInHand.RemoveAt(0);

                //atak-najpierw ruch gracza 1 (gracz 1 atakuje swoją pierwszą kartą pierwszą kartę przeciwnika (dla ułatwienia)), 
                //od punktow zycia karty gracza 2 odejmowane sa punkty ataku karty gracza 1

                player2.cardsOnTable[0].lifePts = player2.cardsOnTable[0].lifePts - player1.cardsOnTable[0].attackPts;

                //jezeli po odjeciu od punktow ataku od punktow zycia liczba punktow zycia spadla < 0, karta wylatuje ze stolu
                if(player2.cardsOnTable[0].lifePts < 0)
                {
                    player2.cardsOnTable.RemoveAt(0);
                }
                
                //atak gracza 2
                player1.cardsOnTable[0].lifePts = player1.cardsOnTable[0].lifePts - player2.cardsOnTable[0].attackPts;

                if (player1.cardsOnTable[0].lifePts < 0)
                {
                    player1.cardsOnTable.RemoveAt(0);
                }


            }
           
            if (player1.lifePts < 0)
            {
                Console.WriteLine("Gracz 1 przegral");
            }
            else if (player2.lifePts < 0)
            {
                Console.WriteLine("Gracz 2 przegral");
            }
            else
            {
                Console.WriteLine("Remis");
            }

        }

    }

}
