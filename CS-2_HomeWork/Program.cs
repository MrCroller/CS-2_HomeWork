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
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 1000;
            form.Height = 800;
            Game.Init(form);
            Game.Draw();
            Application.Run(form);
        }
    }
}
