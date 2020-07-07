using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Match_game_logic;

namespace Match_game_UI
{
    class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            Game game1 = form1.getGame();
            Form2 form2 = new Form2(game1);
            form2.ShowDialog();
            //new Form2(form1.getGame()).ShowDialog();
        }
    }
}
