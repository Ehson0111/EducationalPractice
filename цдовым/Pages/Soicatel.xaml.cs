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
    /// Логика взаимодействия для Soicatel.xaml
    /// </summary>
    public partial class Soicatel : Page
    {
        private static bazaEntities db;
        authorization authorization;
        public Soicatel(authorization user, string role)
        {
            InitializeComponent();
            db = new bazaEntities();
            authorization = user;



            LoadData();
            //text1.Content = user.soiskateli.First();
            time(user);


        }
        private void time(authorization user)
        {
            DateTime currentTime = DateTime.Now;
            string greeting;
            var client = user.soiskateli.First();

            if (currentTime.Hour >= 10 && currentTime.Hour < 12)
            {
                greeting = "Доброе утро";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 17)
            {
                greeting = "Добрый день";
            }
            else if (currentTime.Hour >= 17 && currentTime.Hour < 19)
            {
                greeting = "Добрый вечер";
            }
            else
            {
                greeting = "время";
            }

            text1.Content = $"{greeting}!, {client.Imya} {client.familiya}";
        }

        private void LoadData()
        {
            db = new bazaEntities();
            // Загружаем все данные
            var items = db.Vakansi.ToList();

            ApplyFiltersAndSorting(items);
        }
        private void ApplyFiltersAndSorting(List<Vakansi> items)
        {
            // Поиск
            string searchText = txtSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                items = items
             .Where(x =>
                  x.Opisanie.ToLower().Contains(searchText.ToLower()) || // Filter by address
                  x.nazvaie_vakansii.ToString().ToLower().Contains(searchText.ToLower())  // Filter by housing type
                  )
                 .ToList();
            }

            if (LViewProduct == null)
            {
                return;
            }
            switch (cmbSorting.SelectedIndex)
            {
                case 0:
                    items = items;
                    break;
                case 1:
                    items = items.OrderBy(x => x.zarplata1).ToList();
                    break;
                case 2:
                    items = items.OrderByDescending(x => x.zarplata1).ToList();
                    break;

            }
            // Фильтрация
            switch (cmbFilter.SelectedIndex)
            {
                case 1:
                    items = items.Where(x => x.zarplata1 < 100000).ToList();
                    break;
                case 2:
                    items = items.Where(x => x.zarplata1 >= 100000).ToList();
                    break;
            }

            // Обновляем список и счетчики
            LViewProduct.ItemsSource = items;
            lblCount.Content = $"{items.Count} из {db.Vakansi.Count()}";
        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }
        //private void print_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    var employees = db.Vakansi.ToList();

        //    if (employees.Count == 0)
        //    {
        //        MessageBox.Show("Нет в наличие ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    FlowDocument document = new FlowDocument();

        //    Paragraph header = new Paragraph(new Run("Список недвижимостей"))
        //    {
        //        FontSize = 24,
        //        FontWeight = FontWeights.Bold,
        //        TextAlignment = TextAlignment.Center // Выравнивание по центру
        //    };
        //    document.Blocks.Add(header);


        //    foreach (var employee in employees)
        //    {
        //        Paragraph employeeParagraph = new Paragraph(new Run($"{employee.nazvaie_vakansii }, Адресс , Площадь: {employee.Opisanie}, Стоимость {employee.zarplata1}₽ "))
        //        {
        //            FontSize = 14,
        //            TextAlignment = TextAlignment.Left // Выравнивание по левому краю
        //        };
        //        document.Blocks.Add(employeeParagraph);
        //    }

        //    PrintDialog printDialog = new PrintDialog();
        //    if (printDialog.ShowDialog() == true)
        //    {
        //        // Печатаем документ
        //        IDocumentPaginatorSource idpSource = document;
        //        printDialog.PrintDocument(idpSource.DocumentPaginator, "Список недвижиостей");
        //    }
        //}

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResponseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int vacancyId)
            {
                // Здесь логика отклика на вакансию
                MessageBox.Show($"Отклик отправлен на вакансию ID: {vacancyId}");

                // Пример обновления интерфейса:
                // button.Content = "Отправлено!";
                // button.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is Vakansi vakansi)
            {
                // Получаем ID вакансии
                int vakansiId =Convert.ToInt32( vakansi.id_vakansiy);

                // Получаем ID соискателя (например, из текущей сессии)
                int soiskatelId = Convert.ToInt32(authorization.soiskateli.First().id_soiskatel); // Реализуйте этот метод

                // Статус "Новый" (предположим, что его ID = 1)
                int statusId = 1;

                // Сохраняем отклик в базу данных
                try
                {
                    var helper = new Helpel();
                    helper.CreateOtklik(soiskatelId, vakansiId, statusId);
                    MessageBox.Show("Отклик успешно сохранен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении отклика: {ex.Message}");
                }
            }
        }


    }
}
