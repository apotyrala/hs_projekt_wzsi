using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_projekt_wzsi
{
    class MCTS
    {

        public Node AttackMCTS(Player player, Player enemy, Node node)
        {
                Node current = Selection(node, player.cardsOnTable, player, enemy);

                return current;
        }

        //selekcja 
        public Node Selection(Node current, List<Card> cardsOnTable, Player p, Player e)
        {

            if (p.cardsOnTable.Count != 0)
            {
                //jesli ilosc wezlow potomnych obecnego wezla < liczby kart na stole

                if (current.children == null || current.children.Count < p.cardsOnTable.Count)
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
            AttackRandomMCTS(p, e);
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
            do
            {
                AttackRandomMCTS(player, enemy);

            } while (player.lifePts >= 0);

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

        public Node initMCTS(Player player, Player enemy)
        {
            //wykonaj ruch
            AttackRandomMCTS(player, enemy);
            //zapisz stan gry
            GameState gs = new GameState(player.cardsOnTable, player.cardsInHand, player, player.lifePts, player.manaPts);

            //utworz korzen ze stanem gry (tablica dzieci to cardsOnTable)
            Node root = new Node(null, player.cardsOnTable, gs);

            return root;
        }

        //funkcja do randomowego ruchu gracza MCTS 
        public void AttackRandomMCTS(Player player, Player enemy)
        {
            Random r = new Random();
            int e = r.Next(0, enemy.cardsOnTable.Count);
            Random s = new Random();
            int f = r.Next(0, player.cardsOnTable.Count);

            if (enemy.cardsOnTable.Count != 0) //jesli gracz ma karty na stole
            {
                //losowanie miedzy atakiem w bohatera a atakiem w karty
                Random t = new Random();
                int select = t.Next(0, 2);

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

    }
}
