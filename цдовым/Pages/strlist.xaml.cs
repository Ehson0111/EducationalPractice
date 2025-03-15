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
using WpfApp1;
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


            //employeesDataGrid.ItemsSource = db.Sotrudniki.ToList(); // Привязка данных к ListView
            employeesDataGrid.ItemsSource = Helpel.GetContext().Sotrudniki.ToList();

        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {


            //добавление сотрудника 
            NavigationService.Navigate(new EditEmloyeeForm());
        }
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Обработчик двойного клика по сотруднику изминение данных
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {

                    int employeeId = Convert.ToInt32( selectedEmployee.id_Sotrudnik); // Получение ID сотрудника
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

        
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Helpel.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());

                LoadData();

            }
        }

        private void flowDocementReader_IsVisibleChanged_2(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
         
         
    }
}
