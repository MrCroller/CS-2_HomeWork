using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CS_2_HomeWork
{
    class Program
    {
        public static void Start()
        {
            Form form = new Form();
            Game.Width = 1000;
            Game.Height = 800;
            Game.Init(form);
            Game.Draw();
            Application.Run(form);
        }

        static void Main(string[] args)
        {
            Start();
        }
    }
}
