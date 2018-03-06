

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hs_projekt_wzsi
{

    public class Game
    {

        public List<Card> Deck1 = new List<Card>()
            {
            new Card { lifePts = 1, attackPts = 1, manaPts = 1 },
            new SpecialCard { lifePts = 2, attackPts = 5, manaPts = 2, damagePts =3 },
            new SpecialCard { lifePts = 3, attackPts = 10, manaPts = 3, healPts=1 },
            new Card { lifePts = 2, attackPts = 2, manaPts = 4 },
            new Card { lifePts = 3, attackPts = 3, manaPts = 5 },
            new Card { lifePts = 4, attackPts = 6, manaPts = 6 },
            new Card { lifePts = 5, attackPts = 4, manaPts = 1 },
            new Card { lifePts = 6, attackPts = 3, manaPts = 2 },
            new Card { lifePts = 7, attackPts = 6, manaPts = 3 },
            new Card { lifePts = 8, attackPts = 5, manaPts = 4 },

             };

        public List<Card> Deck2 = new List<Card>()
            {
            new Card { lifePts = 3, attackPts = 1, manaPts = 5 },
            new SpecialCard { lifePts = 4, attackPts = 2, manaPts = 4, damagePts =3},
            new SpecialCard { lifePts = 5, attackPts = 10, manaPts = 3, healPts=1 },
            new Card { lifePts = 3, attackPts = 3, manaPts = 2 },
            new Card { lifePts = 4, attackPts = 3, manaPts = 1 },
            new Card { lifePts = 5, attackPts = 4, manaPts = 1 },
            new Card { lifePts = 6, attackPts = 5, manaPts = 2 },
            new Card { lifePts = 7, attackPts = 6, manaPts = 3 },
            new Card { lifePts = 8, attackPts = 2, manaPts = 4 },
            new Card { lifePts = 9, attackPts = 3, manaPts = 5 },

             };

        private List<Card> ShuffleList<Card>(List<Card> inputList)
        {
            List<Card> randomList = new List<Card>();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                Random r = new Random();
                randomIndex = r.Next(0, inputList.Count); // wybierz randomowy obiekt z losty
                randomList.Add(inputList[randomIndex]); //dodaj go do nowej, randomowej listy
                inputList.RemoveAt(randomIndex); //usun by uniknac duplikatow
            }

            return randomList; //zwroc nowa liste
        }

        public void GamePlayInRandomMode()
        {
            Console.WriteLine("Gracz losowy:");
            Player player1 = new Player(20, 1);
            Player player2 = new Player(20, 1);
            List<Card> shuffledDeck1 = new List<Card>();
            List<Card> shuffledDeck2 = new List<Card>();
            PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);

            int k = 1; //zmienna do odejmowanie punktow zycia gracza
            int mana = 1;//zmienna do dodawania punktow many graczowi

            do
            {
                player1.manaPts = mana;
                player2.manaPts = mana;

                GetCard(player1, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(player2, shuffledDeck2, GetRandomCard(shuffledDeck2));

                //k = k + 1;

                //druga i kolejne tury- gracze rzucaja karte, nie moga jej uzyc (tylko te z poprzednich tur)
                ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                ThrowCard(player2, GetRandomCard(player2.cardsInHand));

                //gracz atakuje drugiego gracza
                AttackRandom(player1, player2);
                //atak gracza 2
                AttackRandom(player2, player1);

                UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);

                if (mana < 10)
                {
                    mana = mana + 1;
                }

            } while (player1.lifePts >= 0 && player2.lifePts >= 0);
            PrintScore(player1, player2);
        }

        public void GamePlayInAgressiveMode()
        {
            Console.WriteLine("Gracz agresywny:");
            Player player1 = new Player(20, 1);
            Player player2 = new Player(20, 1);
            List<Card> shuffledDeck1 = new List<Card>();
            List<Card> shuffledDeck2 = new List<Card>();
            PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);

            int k = 1; //zmienna do odejmowanie punktow zycia gracza
            int mana = 1;//zmienna do dodawania punktow many graczowi

            do
            {
                player1.manaPts = mana;
                player2.manaPts = mana;

                GetCard(player1, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(player2, shuffledDeck2, GetRandomCard(shuffledDeck2));

                //k = k + 1;

                //druga i kolejne tury- gracze rzucaja karte, nie moga jej uzyc (tylko te z poprzednich tur)
                ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                ThrowCard(player2, GetRandomCard(player2.cardsInHand));

                //gracz atakuje drugiego gracza
                AttackCharacter(player1, player2);
                //atak gracza 2
                AttackCharacter(player2, player1);

                UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);

                if (mana < 10)
                {
                    mana = mana + 1;
                }

            } while (player1.lifePts >= 0 && player2.lifePts >= 0);
            PrintScore(player1, player2);
        }

        public void GamePlayInControlMode() // rozgrywka
        {
            Console.WriteLine("Gracz kontrolujacy:");
            Player player1 = new Player(20, 1);
            Player player2 = new Player(20, 1);
            List<Card> shuffledDeck1 = new List<Card>();
            List<Card> shuffledDeck2 = new List<Card>();
            PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);

            int k = 1; //zmienna do odejmowanie punktow zycia gracza
            int mana = 1;//zmienna do dodawania punktow many graczowi

            do
            {

                player1.manaPts = mana;
                player2.manaPts = mana;

                GetCard(player1, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(player2, shuffledDeck2, GetRandomCard(shuffledDeck2));

                //k = k + 1;

                //druga i kolejne tury- gracze rzucaja karte, nie moga jej uzyc (tylko te z poprzednich tur)
                ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                ThrowCard(player2, GetRandomCard(player2.cardsInHand));

                //atak-najpierw ruch gracza 1 (gracz 1 atakuje swoją pierwszą kartą pierwszą kartę przeciwnika (dla ułatwienia)), 
                //od punktow zycia karty gracza 2 odejmowane sa punkty ataku karty gracza 1
                AttackCards(player1, player2);
                //atak gracza 2
                AttackCards(player2, player1);

                UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);

                if (mana < 10)
                {
                    mana = mana + 1;
                }

            } while (player1.lifePts >= 0 && player2.lifePts >= 0);
            PrintScore(player1, player2);
        }

        private void PrepareGame(Player player1, Player player2, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {
            //tasowanie kart
            shuffledDeck1 = ShuffleList(Deck1);
            shuffledDeck2 = ShuffleList(Deck2);

            //rozdanie kart- gracz 1 zaczyna z czterema kartami, gracz 2 z trzema

            for (int i = 0; i < 4; i++)
            {
                GetCard(player1, shuffledDeck1, i);
            }

            for (int i = 0; i < 3; i++)
            {
                GetCard(player2, shuffledDeck2, i);
            }

            UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);


            //pierwsza tura- gracze rzucaja karty na stol (dla ulatwienia pierwsza z listy)- nie mozna jej uzyc w tej samej turze
            ThrowCard(player1, 0);
            ThrowCard(player2, 0);
        }
        private string DisplayCard(Card card)
        {
            return ("Attack: " + card.attackPts.ToString() + ", Life: " + card.lifePts.ToString() + ", Mana: " +
                    card.manaPts.ToString());
        }

        private void GetCard(Player player, List<Card> deck, int nr)
        {
            if (deck.Count != 0) //jezeli mozna jeszcze pobrac karty
            {
                player.cardsInHand.Add(deck[nr]);
                deck.RemoveAt(nr);
                //player.lifePts = player.lifePts - k;
            }
        }

        private void ThrowCard(Player player, int nr)
        {
            if (player.cardsInHand.Count != 0) //jesli gracz ma karty w rece
            {
                if (player.manaPts > 0)
                {
                    player.cardsOnTable.Add(player.cardsInHand[nr]);
                    player.manaPts = player.manaPts - player.cardsInHand[nr].manaPts; //odejmij od punktow many gracza punkty many karty
                    player.cardsInHand.RemoveAt(nr);
                }
            }
        }

        private int GetRandomCard(List<Card> cards)
        {
            Random r = new Random();
            return r.Next(0, cards.Count);
        }

        private void AttackRandom(Player player, Player enemy)
        {
            if (player.cardsOnTable.Count != 0)
            {
                int e = GetRandomCard(enemy.cardsOnTable);
                int f = GetRandomCard(player.cardsOnTable);
                if (enemy.cardsOnTable.Count != 0) //jesli gracz ma karty na stole
                {
                    //losowanie miedzy atakiem w bohatera a atakiem w karty
                    Random r = new Random();
                    int select =  r.Next(0, 2);

                    if (select!= 0)
                        enemy.cardsOnTable[e].lifePts = enemy.cardsOnTable[e].lifePts - player.cardsOnTable[f].attackPts;
                    else
                        enemy.lifePts = enemy.lifePts - player.cardsOnTable[f].attackPts;
                    //jezeli po odjeciu od punktow ataku od punktow zycia liczba punktow zycia spadla < 0, karta wylatuje ze stolu
                    if (enemy.cardsOnTable[e].lifePts < 0)
                    {
                        enemy.cardsOnTable.RemoveAt(e);
                    }
                }
                else
                {
                    enemy.lifePts = enemy.lifePts - player.cardsOnTable[f].attackPts;
                }
            }
        }

        private void AttackCards(Player player, Player enemy)
        {
            if (player.cardsOnTable.Count != 0)
            {
                int e = GetRandomCard(enemy.cardsOnTable);
                int f = GetRandomCard(player.cardsOnTable);
                if (enemy.cardsOnTable.Count != 0) //jesli gracz ma karty na stole
                {

                    enemy.cardsOnTable[e].lifePts = enemy.cardsOnTable[e].lifePts - player.cardsOnTable[f].attackPts;

                    //jezeli po odjeciu od punktow ataku od punktow zycia liczba punktow zycia spadla < 0, karta wylatuje ze stolu
                    if (enemy.cardsOnTable[e].lifePts < 0)
                    {
                        enemy.cardsOnTable.RemoveAt(e);
                    }
                }
                else
                {
                    enemy.lifePts = enemy.lifePts - player.cardsOnTable[f].attackPts;
                }
            }
        }

        private void AttackCharacter(Player player, Player enemy)
        {
            if (player.cardsOnTable.Count != 0)
            {
                //TODO: zrobic ograniczenie
                int f = GetRandomCard(player.cardsOnTable);

                enemy.lifePts = enemy.lifePts - player.cardsOnTable[f].attackPts;
            }
        }

        private void PrintScore(Player player1, Player player2)
        {
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
            Console.ReadKey();
        }

        private void UpdateCardsState(List<Card> pl1d, List<Card> pl2d, List<Card> pl1h, List<Card> pl2h, Player pl1, Player pl2)
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
            Console.WriteLine("");


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
            Console.WriteLine("");
        }
    }
}
