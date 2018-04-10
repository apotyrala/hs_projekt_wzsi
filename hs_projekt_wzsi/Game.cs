﻿

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace hs_projekt_wzsi
{

    public class Game
    {
        List<Card> shuffledDeck1 = new List<Card>();
        List<Card> shuffledDeck2 = new List<Card>();

        public List<Card> Deck1 = new List<Card>()
            {
            new Card { lifePts = 1, attackPts = 1, manaPts = 1 },
            new SpecialCard { lifePts = 2, attackPts = 2, manaPts = 2, damagePts =3 },
            new SpecialCard { lifePts = 3, attackPts = 3, manaPts = 3, healPts=1 },
            new Card { lifePts = 2, attackPts = 2, manaPts = 4 },
            new Card { lifePts = 3, attackPts = 3, manaPts = 5 },
            new Card { lifePts = 4, attackPts = 4, manaPts = 6 },
            new Card { lifePts = 5, attackPts = 4, manaPts = 1 },
            new Card { lifePts = 6, attackPts = 3, manaPts = 2 },
            new Card { lifePts = 7, attackPts = 4, manaPts = 3 },
            new Card { lifePts = 8, attackPts = 1, manaPts = 4 },
            new Card { lifePts = 1, attackPts = 1, manaPts = 1 },

             };

        public List<Card> Deck2 = new List<Card>()
            {
            new Card { lifePts = 1, attackPts = 1, manaPts = 1 },
            new SpecialCard { lifePts = 2, attackPts = 2, manaPts = 2, damagePts =3 },
            new SpecialCard { lifePts = 3, attackPts = 3, manaPts = 3, healPts=1 },
            new Card { lifePts = 2, attackPts = 2, manaPts = 4 },
            new Card { lifePts = 3, attackPts = 3, manaPts = 5 },
            new Card { lifePts = 4, attackPts = 4, manaPts = 6 },
            new Card { lifePts = 5, attackPts = 4, manaPts = 1 },
            new Card { lifePts = 6, attackPts = 3, manaPts = 2 },
            new Card { lifePts = 7, attackPts = 4, manaPts = 3 },
            new Card { lifePts = 8, attackPts = 1, manaPts = 4 },
            new Card { lifePts = 1, attackPts = 1, manaPts = 1 },

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

        //funkcja do wyboru miedzy zwykla rozgrywka, a rozgrywka MCTS
        public void chooseGameMode(int pl1, int pl2)
        {
            Player player1 = new Player(20, 1);
            Player player2 = new Player(20, 1);
            player1.mode = pl1;
            player2.mode = pl2;

            //jezeli ktorys z graczy jest MCTS, przeskakujemy do rozgrywki MCTS funkcję nizej
            if (player1.mode == 4)
            {
                GamePlayMCTS(pl1, pl2, player1, player2, shuffledDeck1, shuffledDeck2);
            }
            else if (player2.mode == 4)
            {
                GamePlayMCTS(pl2, pl1, player2, player1, shuffledDeck2, shuffledDeck1);
            }
            else
            {
                GamePlay(pl1, pl2, player2, player1, shuffledDeck1, shuffledDeck2);
            }
        }

        public void GamePlay(int pl1, int pl2, Player player1, Player player2, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {

            int mana = 1;//zmienna do dodawania punktow many graczowi
             //tasowanie kart
            shuffledDeck1 = ShuffleList(Deck1);
            shuffledDeck2 = ShuffleList(Deck2);

            player1.manaPts = mana;
            player2.manaPts = mana;

            Console.WriteLine(shuffledDeck1.Count);
            Console.WriteLine(shuffledDeck2.Count);
            PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);
            Console.WriteLine(shuffledDeck1.Count);
            Console.WriteLine(shuffledDeck2.Count);

            do
            {
                GetCard(player1, shuffledDeck1, 0);
                GetCard(player2, shuffledDeck2, 0);

                ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                ThrowCard(player2, GetRandomCard(player2.cardsInHand));

                UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);
                Console.WriteLine(shuffledDeck1.Count);
                Console.WriteLine(shuffledDeck2.Count);
                //gracz atakuje drugiego gracza
                if (player1.mode == 1) //losowy
                {
                    Console.WriteLine("Gracz 1 - losowy:");
                    AttackRandom(player1, player2);
                }
                else if (player1.mode == 2)
                {
                    Console.WriteLine("Gracz 1 - agresywny:");
                    AttackCards(player1, player2);
                }
                else if (player1.mode == 3)
                {
                    Console.WriteLine("Gracz 1 - kontrolujacy:");
                    AttackCharacter(player1, player2);
                }


                //atak gracza 2
                if (player2.mode == 1) //losowy
                {
                    Console.WriteLine("Gracz 2 - losowy:");
                    AttackRandom(player2, player1);
                }
                else if (player2.mode == 2)
                {
                    Console.WriteLine("Gracz 2 - agresywny:");
                    AttackCards(player2, player1);
                }
                else if (player2.mode == 3)
                {
                    Console.WriteLine("Gracz 2 - kontrolujacy:");
                    AttackCharacter(player2, player1);
                }

                //dodaj pkt many graczom
                if (player1.manaPts < 10)
                {
                    player1.manaPts = player1.manaPts + 1;
                }
                if (player2.manaPts < 10)
                {
                    player2.manaPts = player2.manaPts + 1;
                }

            } while (player1.lifePts >= 0 && player2.lifePts >= 0);
            PrintScore(player1, player2);

        }
        

        //player- gracz mcts, enemy- nie mcts
        public void GamePlayMCTS(int pl1, int pl2, Player player, Player enemy, List<Card> shuffledDeck1, List <Card> shuffledDeck2)
        {
     
            int value = 0;


            GameState gs = new GameState(player.cardsOnTable, player.cardsInHand, enemy.cardsOnTable, enemy.cardsInHand, player.lifePts, player.manaPts, enemy.lifePts, enemy.manaPts);

            //zapisz do korzenia
            Node root = new Node(null, gs); 
            int x = 0;

            do
            {
                //kopia stanu gry z roota
                //tasowanie kart
                shuffledDeck1 = ShuffleList(Deck1);
                shuffledDeck2 = ShuffleList(Deck2);
                root.gameState.copyState(player, enemy,root.gameState);
                PrepareGame(player, enemy, shuffledDeck1, shuffledDeck2);
                Console.WriteLine("\nRoot value/visits: {0}/{1}", root.value, root.visits);
                Console.WriteLine("\n{0}. iteration", x);

                //obecny wezel to root
                Node current = root;

                //wezel pomocniczy
                Node helper = current;

                helper = Selection(current, player, enemy, shuffledDeck1, shuffledDeck2);
                current = helper;
                current.gameState.copyState(player, enemy, current.gameState);
                Console.WriteLine("Node value/visits: {0}/{1}, Node idx: {2}", current.value, current.visits, current.parent.children.IndexOf(current));

                //dopoki nie osiagnelismy konca drzewa wykonaj rollout
                value = Rollout(current, player, enemy, shuffledDeck1, shuffledDeck2);

                Console.WriteLine("\nplayer lifepts: {0}", player.lifePts);
                Console.WriteLine("\nenemy lifepts: {0}", enemy.lifePts);

                PrintScore(player, enemy);
                Update(current, value);
                x++;

            } while (x < 500);


        }
        
        #region mcts functions

        //selekcja 
        public Node Selection(Node current, Player p, Player e, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {

                Node helper = null;
                //jesli ilosc wezlow potomnych obecnego wezla < liczby kart na stole + 1 (ten plus 1 bo w danej turze gracz może wybrać atak
                //jedną z posiadanych obecnie kart na stole LUB jeden atak bezposrednio w przeciwnika)
                if (current.children == null || current.children.Count < p.cardsOnTable.Count + 1)
                {
                        //ekspansja
                    return Expansion(current, p, e, shuffledDeck1, shuffledDeck2);

                }
                else //dopoki wezel ma dzieci i jest w pelni rozwiniety- wykonuj selekcje
                {

                    //wybor najlepszego dziecka
                    helper = bestChildUCB(current);
                    current = helper;
                    Console.WriteLine("Node value/visits: {0}/{1}", current.value, current.visits);
                    return current;

                }
            
 
       
        }


        //ekspansja
        public Node Expansion(Node current, Player p, Player e, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {

                GetCard(p, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(e, shuffledDeck2, GetRandomCard(shuffledDeck2));

                ThrowCard(p, GetRandomCard(p.cardsInHand));
                ThrowCard(e, GetRandomCard(e.cardsInHand));

                //wykonaj ruch
                AttackRandom(p, e);
                //ruch przeciwnika
                if (e.mode == 1) //losowy
                {
                    AttackRandom(e, p);
                }
                else if (e.mode == 2)
                {
                    AttackCards(e, p);
                }
                else if (e.mode == 3)
                {
                    AttackCharacter(e, p);
                }


                //dodaj pkt many graczom
                if (p.manaPts < 10 )
                {
                    p.manaPts = p.manaPts + 1;
                }
                if (e.manaPts < 10)
                {
                    e.manaPts = e.manaPts + 1;
                }


                //zapisz stan gry
                GameState gs = new GameState(p.cardsOnTable, p.cardsInHand, e.cardsOnTable, e.cardsInHand, p.lifePts, p.manaPts, e.lifePts, e.manaPts);
                //utworz wezel ze stanem gry 
                Node node = new Node(current, gs);

                //dodaj nowy wezel do listy dzieci poprzedniego wezla
                current.addChild(node);

                //nowy stan gry
                node.gameState.copyState(p, e, node.gameState);
                //zwrotka nowego obecnego wezla
                current = node;

                return current;
            

        }

        //wybor najlepszego dziecka- wzor z artykulu
        public Node bestChildUCB(Node current)
        {
            double C = 1.44;
            Node bestChild = null;
            double best = double.NegativeInfinity;

            foreach (Node child in current.children)
            {
                double UCB1 = ((double)child.value / (double)child.visits) + C * Math.Sqrt((2.0 * Math.Log((double)current.visits)) / (double)child.visits);

                if (UCB1 > best)
                {
                    bestChild = child;
                    best = UCB1;
                }
            }

            return bestChild;
        }

        public int Rollout(Node current, Player player, Player enemy, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {
            current.gameState.copyState(player, enemy, current.gameState);

            do
            {

                GetCard(player, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(enemy, shuffledDeck2, GetRandomCard(shuffledDeck2));

                ThrowCard(player, GetRandomCard(player.cardsInHand));
                ThrowCard(enemy, GetRandomCard(enemy.cardsInHand));

                //ruch mcts
                AttackRandom(player, enemy);

                //ruch przeciwnika
                if (enemy.mode == 1) //losowy
                {
                    AttackRandom(enemy, player);
                }
                else if (enemy.mode == 2)
                {
                    AttackCards(enemy, player);
                }
                else if (enemy.mode == 3)
                {
                    AttackCharacter(enemy, player);
                }

                //dodaj pkt many graczom
                if (player.manaPts < 10)
                {
                    player.manaPts = player.manaPts + 1;
                }
                if (enemy.manaPts < 10)
                {
                    enemy.manaPts = enemy.manaPts + 1;
                }


            } while (player.lifePts >= 0 && enemy.lifePts>= 0);

            if (player.lifePts > enemy.lifePts)
            {
                return 1;
            }
            else if (player.lifePts == enemy.lifePts)
            {
                return -0;

            }
            else
            {
                return -1;
            }

            
        }

        //update stanu wezlow
        public void Update(Node current, int value)
        {
            do
            {
                current.visits++;
                current.value += value;
                current = current.parent;
            }
            while (current != null);
        }

        #endregion 

        private void PrepareGame(Player player1, Player player2, List<Card> shuffledDeck1, List<Card> shuffledDeck2)
        {

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

        public int GetRandomCard(List<Card> cards)
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
            else
            {
                enemy.lifePts = enemy.lifePts - 1;
            }
        }


  
        private void PrintScore(Player player1, Player player2)
        {
            if (player1.lifePts < 0)
            {
                Console.WriteLine("Gracz mode {0} przegral", player1.mode);
            }
            else if (player2.lifePts < 0)
            {
                Console.WriteLine("Gracz mode {0} przegral", player2.mode);
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
