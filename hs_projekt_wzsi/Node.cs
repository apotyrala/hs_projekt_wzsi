using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_projekt_wzsi
{
    public class Node
    {
        public Node parent;

        public int value
        {
            get;
            set;
        }
     
        public int visits
        {
            get;
            set;
        }

        public GameState gameState;//stan gry w danym węźle
        public List<Node> children { get; set; } //lista dzieci obecnego wezla

        //konstruktor, wezel przechowuje informacje o swoim rodzicu i stanie gry
        public Node (Node p, GameState gs)
        {
            parent = p;
            gameState = gs;
        }

        //dodawanie wezla potomnego
        public void addChild(Node c)
        {
            this.children.Add(c);
        }
    }

 
}
