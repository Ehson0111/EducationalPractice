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
using цдовым.Models;

namespace цдовым.Pages
{
    /// <summary>
    /// Логика взаимодействия для Sotrudnik.xaml
    /// </summary>
    public partial class Sotrudnik : Page
    {
        public Sotrudnik(authorization user, string role)
        {
            InitializeComponent();
        }

        private void strlist_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new strlist());
        }

        //private void kndlist_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new listclienttov());

        //}

        private void vklist_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Rabotadatel());

        }

    }
}
