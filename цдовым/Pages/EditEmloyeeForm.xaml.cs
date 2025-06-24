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
using WpfApp1.Validators;
using цдовым.Models;

namespace цдовым.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditEmloyeeForm.xaml
    /// </summary>
    public partial class EditEmloyeeForm : Page
    {
        private int _employeeId;
        private bool _isNewEmployee; // Режим добавление или редактирование 
        private static bazaEntities db;
        /// <summary>
        /// Режим редактирование 
        /// 
        /// </summary>
        /// <param name="useriD"></param>
        /// 
        public EditEmloyeeForm()
        {



            InitializeComponent();
            
            _isNewEmployee = true; // Режим добавления
            deluser.Visibility =Visibility.Hidden;
            //db = new bazaEntities();
            db = Helpel.GetContext(); // Используем общий контекст
                                      // Остальной код
            DataContext = new Sotrudniki
            {
                authorization = new authorization()
            };

            _isNewEmployee = true;//режим довление 
            db = new bazaEntities();
            cbDolzhnost.ItemsSource = db.Dolzhnost.ToList(); // Загрузка должностей
            cbpol.ItemsSource = db.pol.ToList(); // Загрузка полов
        }

        public EditEmloyeeForm(int employeeId)
        {

            InitializeComponent();
            _employeeId = employeeId;
            //db = new bazaEntities();
            db = Helpel.GetContext(); // Используем общий контекст

            var employee = db.Sotrudniki.Find(employeeId);
            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }
            DataContext = employee;
            cbDolzhnost.ItemsSource = db.Dolzhnost.ToList();
            cbpol.ItemsSource = db.pol.ToList();

        }
        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtContactData.Clear();
            txtSalary.Clear();
            dpBirthday.SelectedDate = null;
            cbDolzhnost.SelectedIndex = -1;
            cbpol.SelectedIndex = -1;
            pbPassword.Clear();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var employee = DataContext as Sotrudniki;

                if (employee == null)
                {
                    MessageBox.Show("Ошибка при создании или редактировании сотрудника!");
                    return;
                }

                var validator = new EmployeeValidator();
                var validationResults = validator.Validate(employee);
                string error = "Обязательно:";
                
                if (validationResults.Any())
                {
                    foreach (var result in validationResults)
                    {
                        error = error + "\n" + result.ToString();
                    }
                    if (error != "Обязательно:")
                    {
                        MessageBox.Show(error);
                    }
                    return;
                }


                if (_isNewEmployee)
                {
                    // Создание новой авторизации
                    //var newAuth = new authorization
                    //{
                    //    login = employee.authorization.login,
                    //    password = employee.authorization.password,
                    //    //id_authorization = 113,
                    //    id_role = 3 // Убедитесь, что роль с id=3 существует
                    //}; 

                    //db.authorization.Add(newAuth);
                    //db.SaveChanges(); // Сохраняем, чтобы получить id_authorization

                    //// Привязываем id к сотруднику
                    //employee.id_authorization = newAuth.id_authorization;

                    //// Проверка должности
                    ///
                    HashPassword hash = new HashPassword();
                    Helpel helpel = new Helpel();
                    employee.authorization.password = hash.HashPassword1(pbPassword.Text);
                    //employee.authorization.id_authorization = 118;
                    employee.authorization.id_authorization = helpel.GetLastAuthorizationId()+1;
                    employee.id_Sotrudnik = helpel.GetLastAuthorizationId()+1;
                    //employee.authorization.id_authorization  = hash.HashPassword1(pbPassword.Text);
                    if (cbDolzhnost.SelectedValue != null && int.TryParse(cbDolzhnost.SelectedValue.ToString(), out int dolzhnostId))
                    {
                        employee.id_dolzhnost = dolzhnostId;
                    }
                    else
                    {
                        MessageBox.Show("Не выбрана должность!");
                        return;
                    }

                    // Проверка пола
                    if (cbpol.SelectedValue != null && int.TryParse(cbpol.SelectedValue.ToString(), out int polId))
                    {
                        employee.id_pol = polId;
                    }
                    else
                    {
                        MessageBox.Show("Не выбран пол!");
                        return;
                    }

                    // Добавление сотрудника
                    db.Sotrudniki.Add(employee);
                    db.SaveChanges();
                }
                else
                {
                    // Редактирование существующего сотрудника
                    var existingEmployee = db.Sotrudniki.Find(_employeeId);
                    if (existingEmployee == null)
                    {
                        MessageBox.Show("Сотрудник не найден!");
                        return;
                    }

                    existingEmployee.Imya = txtFirstName.Text;
                    existingEmployee.Familiya = txtLastName.Text;
                    existingEmployee.Otchestvo = txtMiddleName.Text;
                    existingEmployee.kontakti = txtContactData.Text;
                    existingEmployee.zarplata = Convert.ToInt32(txtSalary.Text);
                    existingEmployee.data_rozhdenie = dpBirthday.SelectedDate.Value;

                    if (cbDolzhnost.SelectedValue != null && int.TryParse(cbDolzhnost.SelectedValue.ToString(), out int dolzhnostId))
                    {
                        existingEmployee.id_dolzhnost = dolzhnostId;
                    }
                    else
                    {
                        MessageBox.Show("Не выбрана должность!");
                        return;
                    }

                    if (cbpol.SelectedValue != null && int.TryParse(cbpol.SelectedValue.ToString(), out int polId))
                    {
                        existingEmployee.id_pol = polId;
                    }
                    else
                    {
                        MessageBox.Show("Не выбран пол!");
                        return;
                    }

                    HashPassword hash = new HashPassword();
                    existingEmployee.authorization.password = hash.HashPassword1(pbPassword.Text);
                    db.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены!");
                NavigationService.GoBack();
            }
         
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void txtContactData_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void deluser_Click(object sender, RoutedEventArgs e)
        {
            //_employeeId
            Helpel helpel = new Helpel();
            helpel.RemoveUser(_employeeId);
            db.SaveChanges();
            NavigationService.GoBack();

        }

        /// <summary>
        /// Режим добавление 
        /// 
        /// </summary>

    }
}
