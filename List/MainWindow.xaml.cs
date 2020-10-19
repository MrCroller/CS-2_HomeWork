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
        public ObservableCollection<People> Cl;
        public string Select { get; set; }
        private bool flagSelect { get; set; }
        public string Edit { get; set; }

        public MainWindow()
        {
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
            //SpisOk_DP.ItemsSource = Cl.Distinct(i => i.Departament);
            UpdateD();
        }

        /// <summary>
        /// Создание string списка организаций на основе имющихся
        /// </summary>
        private void UpdateD()
        {
            // Оснавная суть данного костыля убрать дубликаты
            var selected = from i in Cl
                                select i.Departament;
            SpisOk_DP.ItemsSource = selected.Distinct();
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
                MessageBox.Show("данные введены некорректно");
            }
            DP_TextBox.Clear();
            P_TextBox.Clear();
            UpdateD();
        }

        /// <summary>
        /// Нажатие на список департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpisOk_DP_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            flagSelect = true; // А этот костыль для переименования Департамента или Имени человека
            Select = SpisOk_DP.SelectedItem as string;

            var list = from t in Cl
                       where t.Departament == Select
                       select t.Name;

            SpisOk_P.ItemsSource = list;

            // Rename.Visibility = Visibility.Visible;
        }

        private void SpisOk_P_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            flagSelect = false;
            Select = SpisOk_P.SelectedItem as string;
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            var frm = new Edit(this);
            frm.ShowDialog();

            foreach (People P in Cl)
            {
                if (flagSelect)
                {
                    if (P.Departament == Select) P.Departament = Edit;
                }
                else
                {
                    if (P.Name == Select) P.Name = Edit;
                }
            }
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
    }
}
