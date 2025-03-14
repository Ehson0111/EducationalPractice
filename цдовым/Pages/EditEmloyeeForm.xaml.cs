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
            db = new bazaEntities();

            _isNewEmployee = true;//режим довление 
            db=new bazaEntities();

            cbDolzhnost.ItemsSource = db.Dolzhnost.ToList(); // Загрузка должностей
            cbpol.ItemsSource = db.pol.ToList(); // Загрузка полов
        }

        public EditEmloyeeForm(int useriD)
        {

            InitializeComponent();
             
        }


        /// <summary>
        /// Режим добавление 
        /// 
        /// </summary>
        
    }
}
