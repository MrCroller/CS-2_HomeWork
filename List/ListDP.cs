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
        public ObservableCollection<People> Dep { get; set; }

        public ListDP() => Dep = new ObservableCollection<People>()
            {
                new People(){Departament = "Компания Гачи", Name = "Билли Хэрингтон" },
                new People(){Departament = "ООО Буравчик"}
            };

        public void DCreate(string s)
        {
            Dep.Add(new People() { Departament = s });

        }

        public void PAdd(string s)
        {
            Dep.Add(new People() { Name = s });

        }
    }
}
