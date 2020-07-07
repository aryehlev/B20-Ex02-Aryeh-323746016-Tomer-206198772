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
            //new Form2(form1.getGame()).ShowDialog();
        }
    }
}
