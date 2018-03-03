

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hs_projekt_wzsi
{

    public class Game
    {

        public List<Card> Deck1 = new List<Card>()
            {
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new SpecialCard { lifePts = 1, attackPts = 10, manaPts = 5, damagePts =3 },
            new SpecialCard { lifePts = 1, attackPts = 10, manaPts = 5, healPts=1 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 1, attackPts = 10, manaPts = 5 },

             };

        public List<Card> Deck2 = new List<Card>()
            {
            new Card { lifePts = 3, attackPts = 10, manaPts = 5 },
            new SpecialCard { lifePts = 4, attackPts = 10, manaPts = 5, damagePts =3},
            new SpecialCard { lifePts = 5, attackPts = 10, manaPts = 5, healPts=1 },
            new Card { lifePts = 3, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 4, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 5, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 6, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 7, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 8, attackPts = 10, manaPts = 5 },
            new Card { lifePts = 9, attackPts = 10, manaPts = 5 },

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
            Random r = new Random();
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
                player1.cardsInHand.Insert(i,shuffledDeck1[i]);
                shuffledDeck1.RemoveAt(i); //usuniecie pobranych kart
            }

            for (int i = 0; i < 3; i++)
            {
                player2.cardsInHand.Insert(i, shuffledDeck2[i]);
                shuffledDeck2.RemoveAt(i);
            }

            updateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);


            //pierwsza tura- gracze rzucaja karty na stol (dla ulatwienia pierwsza z listy)- nie mozna jej uzyc w tej samej turze
            player1.cardsOnTable.Insert(0, player1.cardsInHand[0]);
            player1.cardsInHand.RemoveAt(0);//karta usunieta z reki
            player2.cardsOnTable.Insert(0, player2.cardsInHand[0]);
            player2.cardsInHand.RemoveAt(0);
            
            do
            {
                int k = 1; //zmienna do odejmowanie punktow zycia gracza

                if (shuffledDeck1.Count != 0)  //jezeli mozna jeszcze pobrac karty
                {
                    int a = r.Next(0, shuffledDeck1.Count);
                    //dobieranie karty(dla ulatwienia pierwsza z listy)
                    player1.cardsInHand.Add(shuffledDeck1[a]);
                    shuffledDeck1.RemoveAt(a);//karta usunieta z puli
                    player1.lifePts = player1.lifePts - k;//odjecie punktow zycia graczowi za pobranie karty- po kazdej turze o 1 wiecej
                }

                if (shuffledDeck2.Count != 0)
                {
                    int b = r.Next(0, shuffledDeck2.Count);
                    player2.cardsInHand.Add(shuffledDeck2[b]);
                    shuffledDeck2.RemoveAt(b);
                    player2.lifePts = player2.lifePts - k;
                }

                k = k + 1;

                if (player1.cardsInHand.Count != 0) //jesli gracz ma karty w rece
                {
                    int c = r.Next(0, player1.cardsInHand.Count);
                    //druga i kolejne tury- gracze rzucaja karte, nie moga jej uzyc (tylko te z poprzednich tur)
                    player1.cardsOnTable.Insert(0, player1.cardsInHand[c]);
                    player1.cardsInHand.RemoveAt(c);//karta usunieta z reki
                }

                if (player2.cardsInHand.Count != 0)
                {
                    int d = r.Next(0, player2.cardsInHand.Count);
                    player2.cardsOnTable.Insert(0, player2.cardsInHand[d]);
                    player2.cardsInHand.RemoveAt(d);
                }
                //atak-najpierw ruch gracza 1 (gracz 1 atakuje swoją pierwszą kartą pierwszą kartę przeciwnika (dla ułatwienia)), 
                //od punktow zycia karty gracza 2 odejmowane sa punkty ataku karty gracza 1

                if (player1.cardsOnTable.Count != 0)
                {
                    int e = r.Next(0, player2.cardsOnTable.Count);
                    int f = r.Next(0, player1.cardsOnTable.Count);
                    if (player2.cardsOnTable.Count != 0) //jesli gracz ma karty na stole
                    {

                        player2.cardsOnTable[e].lifePts = player2.cardsOnTable[e].lifePts - player1.cardsOnTable[f].attackPts;

                        //jezeli po odjeciu od punktow ataku od punktow zycia liczba punktow zycia spadla < 0, karta wylatuje ze stolu
                        if (player2.cardsOnTable[e].lifePts < 0)
                        {
                            player2.cardsOnTable.RemoveAt(e);
                        }
                    }
                    else
                    {
                        player2.lifePts = player2.lifePts - player1.cardsOnTable[f].attackPts;
                    }
                }
                //atak gracza 2

                if (player2.cardsOnTable.Count != 0)
                {
                    int g = r.Next(0, player1.cardsOnTable.Count);
                    int h = r.Next(0, player2.cardsOnTable.Count);
                    if (player1.cardsOnTable.Count != 0)
                    {
                        player1.cardsOnTable[g].lifePts = player1.cardsOnTable[g].lifePts - player2.cardsOnTable[h].attackPts;

                        if (player1.cardsOnTable[g].lifePts < 0)
                        {
                            player1.cardsOnTable.RemoveAt(g);
                        }
                    }
                    else
                    {
                        player1.lifePts = player1.lifePts - player2.cardsOnTable[h].attackPts;
                    }
                }
                updateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);
            } while (player1.lifePts >= 0 && player2.lifePts >= 0);


            if (player1.lifePts < 0)
            {
                //l.Text = "Gracz 1 przegral";
                Console.WriteLine("Gracz 1 przegral");
            }
            else if (player2.lifePts < 0)
            {
                //l.Text ="Gracz 2 przegral";
                Console.WriteLine("Gracz 2 przegral");
            }
            else
            {
                //l.Text = "Remis";
                Console.WriteLine("Remis");
            }
            Console.ReadKey();

        }

        public string DisplayCard(Card card)
        {
            return ("Attack: " + card.attackPts.ToString() + ", Life: " + card.lifePts.ToString() + ", Mana: " +
                    card.manaPts.ToString());
        }

        public void updateCardsState(List<Card> pl1d, List<Card> pl2d, List<Card> pl1h, List<Card> pl2h, Player pl1, Player pl2)
        {
            Console.WriteLine("Karty w rece Garcza 1:");
            foreach (var card in pl1h)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Karty w talii Gracza 1: ");
            foreach (var card in pl1d)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Karty na stole Gracza 1: ");
            foreach (var card in pl1.cardsOnTable)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Punkty zycia Gracza 1: " + pl1.lifePts.ToString());


            //gracz 2
            Console.WriteLine("Karty w rece Garcza 2:");
            foreach (var card in pl2h)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Karty w talii Gracza 2:");
            foreach (var card in pl2d)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Karty na stole Gracza 2:");
            foreach (var card in pl2.cardsOnTable)
            {
                Console.WriteLine(DisplayCard(card));
            }
            Console.WriteLine("Punkty zycia Gracza 2: " + pl2.lifePts.ToString());
            Console.WriteLine("***********************************************************************");
        }
    }

}
