using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_projekt_wzsi
{
    class Tree
    {
        public Node root;

        public List<Node> nodeList;//wszystkie wezly w drzewie

        public Tree(Node r)
        {
            root = r;
        }

        //dodawanie wezlow do drzewa
        public void addNode(Node n)
        {
            this.nodeList.Add(n);
        }
    }
}
