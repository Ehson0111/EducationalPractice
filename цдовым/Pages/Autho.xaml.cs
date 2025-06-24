using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
using System.Windows.Threading;
using WpfApp1;
using WpfApp1.Services;
using цдовым.Models;

namespace цдовым.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    /// 


    public partial class Autho : Page
    {
        int click; 
        string checkCode; 
        private static bazaEntities db;
        int n;
        private DispatcherTimer timer; // Таймер для блокировки входа
        private int timeLeft; // Оставшееся время блокировки
        public Autho()
        {
            InitializeComponent();
            click = 0; // Инициализация счетчиков
            n = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Интервал таймера
            timer.Tick += Timer_Tick; // Обработчик события таймера
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft > 0)
            {
                tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            }
            else
            {
                timer.Stop(); // Остановка таймера
                tbTimeLeft.Visibility = Visibility.Hidden; // Скрытие текста таймера
                tbLogin.IsEnabled = true; // Разблокировка полей
                tbPassword.IsEnabled = true;
                btnEnterGuest.IsEnabled = true;
                tbCaptcha.IsEnabled = true;
                btnEnter.IsEnabled = true;
                n = 0; // Сброс счетчика неудачных попыток
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {

            click = +1;
            string login1 = tbLogin.Text.Trim();
            string password1 = tbPassword.Text.Trim();

            HashPassword hash = new HashPassword();
            //password1 = hash.HashPassword1(password1);


            //password1 = hash.HashPassword1(password1);


            db = new bazaEntities();
            //Запрос для поиска пользователей
            var user = db.authorization.Where(x => x.login == login1 && x.password == password1).FirstOrDefault();

            if (click == 1)
            {
                if (user != null)

                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user); // Переход на страницу в зависимости от роли
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha(); // Генерация капчи
                    tbLogin.Text = " ";
                    tbPassword.Text = " ";
                    tbCaptcha.Text = " ";
                    n++;
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text.Trim() == tblCaptcha.Text.Trim())
                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user);
                }
                else
                {
                    if (n >= 3)
                    {
                        locked(); // Блокировка входа
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        GenerateCapctcha();
                        n++;
                        tbLogin.Clear();
                        tbPassword.Clear();
                        tbCaptcha.Clear();
                    }
                }
            }
        }
        private void locked()
        {
            MessageBox.Show("Блокировка: Слишком много неудачных попыток входа.");
            tbLogin.IsEnabled = false;
            tbPassword.IsEnabled = false;
            btnEnter.IsEnabled = false;
            btnEnterGuest.IsEnabled = false;
            tbCaptcha.IsEnabled = false;
            tbTimeLeft.Visibility = Visibility.Visible;
            timeLeft = 10;
            tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            timer.Start(); // Запуск таймера
        }
        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;
            string capctchaText = CapthchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }
        private void LoadPage(string _role, authorization user)
        {
            click = 0;

            //if (tbcode.Text == checkCode)
            //{
            //    clicn += 1;

            switch (_role)
            {
                case "Соискатель":
                    NavigationService.Navigate(new Soicatel(user, _role));
                    break;

                case "Сотрудник":
                    NavigationService.Navigate(new Sotrudnik(user, _role));
                    break;

                case "Работадатель":
                    NavigationService.Navigate(new Rabotadatel(user, Convert.ToInt32(user.soiskateli.First().id_soiskatel),_role));
                    break;
            }
            //}
        }
        private string GenerateFourDigitCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Soicatel());


        }
    }
}
