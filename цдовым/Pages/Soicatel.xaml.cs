﻿using System;
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
    /// Логика взаимодействия для Soicatel.xaml
    /// </summary>
    public partial class Soicatel : Page
    {
        public Soicatel(authorization user, string role)
        {
            InitializeComponent();
        }
    }
}
