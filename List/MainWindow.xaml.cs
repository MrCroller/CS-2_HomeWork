using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Collections.ObjectModel;

namespace List
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public People Select { get; set; }
        ObservableCollection<People> Cl;
        ObservableCollection<string> Dep;
        ObservableCollection<string> Ppl;

        public MainWindow()
        {
            Select = new People();
            InitializeComponent();
            big_chungus.Play();
            Cl = new ObservableCollection<People>
            #region Заполнение коллекции
            {
                new People(){Departament = "Компания Гачи", Name = "Билли Хэрингтон" },
                new People(){Departament = "Компания Гачи", Name = "Вкусный дед" },
                new People(){Departament = "ООО Буравчик", Name = "Маслёнок 007"},
                new People(){Departament = "ООО Безбаб"}
            };
            #endregion
            Update();
        }

        private void Update()
        {

            SpisOk_DP.ItemsSource = Cl;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DP_TextBox.Text))
            {
                Cl.Add(new People()
                { 
                    Departament = DP_TextBox.Text,
                    Name = P_TextBox.Text
                });
            }
            else
            {
                MessageBox.Show("Название введено некорректно");
            }
            DP_TextBox.Clear();
            P_TextBox.Clear();
        }

        private void lbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Метод для воспроизведения гифок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void big_chungus_MediaEnded(object sender, RoutedEventArgs e)
        {
            big_chungus.Position = TimeSpan.FromMilliseconds(1);
        }

        /// <summary>
        /// Нажатие на список департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpisOk_DP_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var Select = SpisOk_DP.SelectedItem as People;
            var list = from t in Cl
                       where t.Departament == Select.Departament
                       orderby t
                       select t.Name;

            MainGrid.DataContext = list;
        }

        private void SpisOk_P_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
