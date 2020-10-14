using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class Departament
    {
        public string Name { get; set; }

        public List<string> People { get; set; }

        public void P_Add(string s)
        {
            People.Add(s);
        }
    }
}
