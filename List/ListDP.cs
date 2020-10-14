using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class ListDP
    {
        public ObservableCollection<Departament> Dep { get; set; }

        public ListDP() => Dep = new ObservableCollection<Departament>()
            {
                new Departament(){Name = "Компания Гачи"},
                new Departament(){Name = "ООО Буравчик"}
            };

        public void Create(string s)
        {
            Dep.Add(new Departament() { Name = s });

        }
    }
}
