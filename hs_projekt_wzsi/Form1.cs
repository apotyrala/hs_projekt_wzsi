using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hs_projekt_wzsi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //do zmiany - na razie jest to tylko do podgladu

            Game game = new Game();

            List<Card> shuffledDeck1 = new List<Card>();
        
            shuffledDeck1 = game.ShuffleList(game.Deck1);
          
            foreach (Card c in shuffledDeck1)
            {
                textBox1.Text += "Card life: " + c.lifePts + "\r\n";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
