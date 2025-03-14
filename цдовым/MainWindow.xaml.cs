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
using цдовым.Pages;

namespace цдовым
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fr_content.Content = new Autho();

            //CheckDatabaseConnection(); // Проверка подключения при запуске


        }
        private void CheckDatabaseConnection()
        {
            try
            {
                using (var db = new bazaEntities())
                {
                    // Пытаемся выполнить простой запрос к базе данных
                    int count = db.rabotadatel.Count();

                    DataGrid datagrid = new DataGrid();
                    datagrid.ItemsSource= db.rabotadatel.ToList();
                    // Если запрос выполнен успешно, выводим сообщение
                    MessageBox.Show($"Подключение к базе данных успешно установлено. Записей в таблице rabotadatel: {count}");
                }
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, выводим сообщение об ошибке


                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)


        {
            fr_content.GoBack();


        }

        private void fr_content_Navigated(object sender, NavigationEventArgs e)
        {
            if (fr_content.CanGoBack)
                btnBack.Visibility = Visibility.Visible;
            else
                btnBack.Visibility = Visibility.Hidden;


        }

        private void fr_content_ContentRendered(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
