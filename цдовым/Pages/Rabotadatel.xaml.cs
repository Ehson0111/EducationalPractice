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
    /// Логика взаимодействия для Rabotadatel.xaml
    /// </summary>
    public partial class Rabotadatel : Page
    {
        private bazaEntities _context;
        private int _currentUserId; // ID текущего пользователя
        private string _currentUserRole; // Роль текущего пользователя
        int soisctael;
        bool isall;
        private static bazaEntities db;
        public Rabotadatel(authorization user, int userId, string userRole)
        {
            InitializeComponent();
            soisctael = Convert.ToInt32(user.soiskateli.First().id_soiskatel);
            _context = new bazaEntities(); // Инициализация контекста базы данных
            _currentUserId = userId; // Сохраняем ID пользователя
            _currentUserRole = userRole; // Сохраняем роль пользовател
        }
        public Rabotadatel()
        {
            InitializeComponent();
            isall = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOtkliki(); // Загрузка данных при загрузке страницы
        }

        // Метод для загрузки от

        // Метод для загрузки откликов
        private void LoadOtkliki()
        {

            if (isall)


            {
                db = new bazaEntities();
                //var otklikiQuery = _context.Otklki_Soskateley.AsQueryable();
                var otklikiQuery = db.Otklki_Soskateley;
                // Фильтрация в зависимости от роли пользователя


                // Преобразуем данные для отображения
                var otkliki = otklikiQuery
                    .Select(o => new
                    {
                        o.id_otklil,
                        o.id_soiskatel,
                        o.id_vakansi,
                        //o.id_status,
                        o.status,
                        o.data_otklik,
                        SoiskatelName = o.soiskateli.familiya + " " + o.soiskateli.Imya, // Имя соискателя
                        VakansiyaName = o.Vakansi.nazvaie_vakansii // Название вакансии
                    }).OrderByDescending(x=>x.data_otklik)
                    .ToList();

                // Привязываем данные к ItemsControl
                ItemsControlOtkliki.ItemsSource = otkliki;



            }
            else
            {
                var otklikiQuery = _context.Otklki_Soskateley.AsQueryable();

                // Фильтрация в зависимости от роли пользователя
                
     
                // Преобразуем данные для отображения
                var otkliki = otklikiQuery
                    .Select(o => new
                    {
                        o.id_otklil,
                        o.id_soiskatel,
                        o.id_vakansi,
                        //o.id_status,
                        o.status,
                        o.data_otklik,
                        SoiskatelName = o.soiskateli.familiya + " " + o.soiskateli.Imya, // Имя соискателя
                        VakansiyaName = o.Vakansi.nazvaie_vakansii // Название вакансии
                    }).Where(x => x.id_soiskatel == soisctael)
                    .ToList();

                // Привязываем данные к ItemsControl
                ItemsControlOtkliki.ItemsSource = otkliki;
            }

        }
    }
}
