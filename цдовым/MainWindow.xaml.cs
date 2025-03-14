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
            CheckDatabaseConnection(); // Проверка подключения при запуске

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
    }
}
