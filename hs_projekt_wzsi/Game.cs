

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

        public void GamePlay(int pl1, int pl2)
        {
            Player player1 = new Player(20, 1);
            Player player2 = new Player(20, 1);
            List<Card> shuffledDeck1 = new List<Card>();
            List<Card> shuffledDeck2 = new List<Card>();
            PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);
            player1.mode = pl1;
            player2.mode = pl2;

            int k = 1; //zmienna do odejmowanie punktow zycia gracza
            int mana = 1;//zmienna do dodawania punktow many graczowi

            do
            {
                player1.manaPts = mana;
                player2.manaPts = mana;

                GetCard(player1, shuffledDeck1, GetRandomCard(shuffledDeck1));
                GetCard(player2, shuffledDeck2, GetRandomCard(shuffledDeck2));

                ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                ThrowCard(player2, GetRandomCard(player2.cardsInHand));
                 
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
                else if (player1.mode == 4)
                {
                    Console.WriteLine("Gracz 1 - MCTS:");
                    GamePlayMCTS(pl1, pl2, player1, player2, shuffledDeck1, shuffledDeck2);
                    break;

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
                else if (player2.mode == 4)
                {
                    Console.WriteLine("Gracz 1 - MCTS:");
                    GamePlayMCTS(pl1, pl2, player1,player2,shuffledDeck1,shuffledDeck2);
                    break;

                }


                UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);

                if (mana < 10)
                {
                    mana = mana + 1;
                }

            } while (player1.lifePts >= 0 && player2.lifePts >= 0);
            PrintScore(player1, player2);
        }

        public void GamePlayMCTS(int pl1, int pl2, Player player1, Player player2, List<Card> shuffledDeck1, List <Card> shuffledDeck2)
        {
            for (int i = 0; i < 100; i++)
            {
                PrepareGame(player1, player2, shuffledDeck1, shuffledDeck2);
                player1.mode = pl1;
                player2.mode = pl2;

                int k = 1; //zmienna do odejmowanie punktow zycia gracza
                int mana = 1;//zmienna do dodawania punktow many graczowi


                do
                {
                    player1.manaPts = mana;
                    player2.manaPts = mana;

                    GetCard(player1, shuffledDeck1, GetRandomCard(shuffledDeck1));
                    GetCard(player2, shuffledDeck2, GetRandomCard(shuffledDeck2));

                    ThrowCard(player1, GetRandomCard(player1.cardsInHand));
                    ThrowCard(player2, GetRandomCard(player2.cardsInHand));

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
                    else if (player1.mode == 4)
                    {
                        Console.WriteLine("Gracz 1 - MCTS:");
                        AttackMCTS(player1, player2);
                        break;

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
                    else if (player2.mode == 4)
                    {
                        Console.WriteLine("Gracz 1 - MCTS:");
                        AttackMCTS(player2, player1);
                        break;

                    }


                    UpdateCardsState(shuffledDeck1, shuffledDeck2, player1.cardsInHand, player2.cardsInHand, player1, player2);

                    if (mana < 10)
                    {
                        mana = mana + 1;
                    }

                } while (player1.lifePts >= 0 && player2.lifePts >= 0);
                PrintScore(player1, player2);
            }
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

        //funkcja do randomowego ruchu gracza MCTS 
        private void AttackRandomMCTS(Player player, Player enemy)
        {
                int e = GetRandomCard(enemy.cardsOnTable);
                int f = GetRandomCard(player.cardsOnTable);
                if (enemy.cardsOnTable.Count != 0) //jesli gracz ma karty na stole
                {
                    //losowanie miedzy atakiem w bohatera a atakiem w karty
                    Random r = new Random();
                    int select = r.Next(0, 2);

                    if (select != 0)
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

        private Node initMCTS(Player player, Player enemy)
        {
            //wykonaj ruch
            AttackRandomMCTS(player, enemy);
            //zapisz stan gry
            GameState gs = new GameState(player.cardsOnTable, player.cardsInHand, player, player.lifePts, player.manaPts);

            //utworz korzen ze stanem gry (tablica dzieci to cardsOnTable)
            Node root = new Node(null, player.cardsOnTable, gs);

            return root;
        }
        private void AttackMCTS(Player player, Player enemy, Node root)
        {
            if (player.cardsOnTable.Count != 0)
            {
 
                Node current = Selection(root, player.cardsOnTable, player, enemy);
                root = current;
                int value = Rollout(current, player, enemy);
                Update(current, value);
                
            }

        }

        //selekcja 
        public Node Selection(Node current, List<Card> cardsOnTable, Player p, Player e)
        {

            if (p.cardsOnTable.Count != 0)
            {
                //jesli ilosc wezlow potomnych obecnego wezla < liczby kart na stole
                
                if(current.children == null || current.children.Count< p.cardsOnTable.Count)
                {
                    //ekspansja
                    return Expansion(current, p.cardsOnTable, p, e);
                }
                else
                {
                    //wybor najlepszego dziecka
                    current = bestChildUCB(current);
                }
               
            }
            return current;
        }

        //ekspansja
        public Node Expansion(Node current, List<Card> cardsOnTable, Player p, Player e)
        {
         
                //wykonaj ruch
                AttackRandom(p, e);
                //zapisz stan gry
                GameState gs = new GameState(p.cardsOnTable, p.cardsInHand, p, p.lifePts, p.manaPts);
                //utworz wezel ze stanem gry (tablica dzieci to cardsOnTable)
                Node node = new Node(current, cardsOnTable, gs);
                //
                current = node;
                return node;
         
        
        }

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

        public int Rollout(Node current, Player player, Player enemy)
        {
            AttackRandom(player, enemy);

            if (player.lifePts < enemy.lifePts)
            {
                return -1;
            }
            else if (player.lifePts > enemy.lifePts)
            {
                return 1;
            }

            else return 0;
        }

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
