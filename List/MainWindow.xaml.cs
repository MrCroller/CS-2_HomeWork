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

namespace List
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListDP Departament;
        public MainWindow()
        {
            InitializeComponent();
            big_chungus.Play();
            Departament = new ListDP();
            MainGrid.DataContext = Departament;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Departament.Dep.Add();

            P_TextBox.Clear();
        }

        private void lbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateDep_Click(object sender, RoutedEventArgs e)
        {
            Departament.Create(DP_TextBox.Text);

            DP_TextBox.Clear();
        }

        private void big_chungus_MediaEnded(object sender, RoutedEventArgs e)
        {
            big_chungus.Position = TimeSpan.FromMilliseconds(1);
        }

        private void SpisOk_DP_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Departament = SpisOk_DP.SelectedItem as ListDP;
            //var frm = new WindowEdt(this);
        }
    }
}
