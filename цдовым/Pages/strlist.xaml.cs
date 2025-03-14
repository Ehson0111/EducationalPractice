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
    /// Логика взаимодействия для strlist.xaml
    /// </summary>
    public partial class strlist : Page
    {
        private static bazaEntities db;
        public strlist()
        {
            InitializeComponent();
            db = new bazaEntities(); // Инициализация контекста
            LoadData(); // Загрузка данных
        }
        private void LoadData() {

            //var employees = db.Sotrudniki.Select(c => new
            //{
            //    c.id_Sotrudnik,
            //    c.Imya,
            //    c.Familiya,
            //    c.Otchestvo,
            //    c.kontakti,
            //    //nazvanie = c.Dolzhnost .Dolzhnoest_namimenovanie
            //    c.Dolzhnost.Dolzhnoest_namimenovanie
            //}).ToList();
            employeesDataGrid.ItemsSource = db.Sotrudniki.ToList(); // Привязка данных к ListView
        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {


            //NavigationService.Navigate(new EditEmployeeForm());
            NavigationService.Navigate(new EditEmloyeeForm());
        }
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Обработчик двойного клика по сотруднику
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {
                    int employeeId = selectedEmployee.Id_Сотрудник; // Получение ID сотрудника
                    NavigationService.Navigate(new EditEmloyeeForm(employeeId)); // Переход на страницу редактирования
                }
            }
        }
        private void PrintListButton_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = flowDocementReader.Document;
            if (doc == null)
            {
                MessageBox.Show("Документ не найден");
                return;

            }

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Список сотрудников");
            }
        }

        private void flowDocementReader_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void flowDocementReader_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
