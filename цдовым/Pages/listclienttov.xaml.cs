using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfApp1;
using цдовым.Models;

namespace цдовым.Pages
{
    /// <summary>
    /// Логика взаимодействия для listclienttov.xaml
    /// </summary>
    public partial class listclienttov : Page
    {
        private static bazaEntities db;
        public listclienttov()
        {
            InitializeComponent();
            db = new bazaEntities(); // Инициализация контекста
            LoadData(); // Загрузка данных
        }
        private void LoadData()
        {


            //employeesDataGrid.ItemsSource = db.Sotrudniki.ToList(); // Привязка данных к ListView
            //var sotrudniki = Helpel.GetContext().rabotadatel.ToList();
            var soiskateli = Helpel.GetContext().soiskateli.ToList();
            employeesDataGrid.ItemsSource = soiskateli;
      
        }
    
        private void adduser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void flowDocementReader_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void PrintListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Обработчик двойного клика по сотруднику изминение данных
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {

                    int employeeId = Convert.ToInt32(selectedEmployee.id_soiskatel); // Получение ID сотрудника
                    NavigationService.Navigate(new EditEmloyeeForm(employeeId)); // Переход на страницу редактирования
                }
            }
        }
    }
}
