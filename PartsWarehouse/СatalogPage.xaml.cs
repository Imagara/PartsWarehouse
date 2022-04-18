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

namespace PartsWarehouse
{
    /// <summary>
    /// Логика взаимодействия для СatalogPage.xaml
    /// </summary>
    public partial class СatalogPage : Page
    {
        public СatalogPage(string carCompany = null, string carName = null, string carGeneration = null)
        {
            InitializeComponent();
            if(carCompany != null && carName != null)
            {
                CarCompanyCombo.Text = carCompany;
                CarNameCombo.Text = carName;
                CarGenerationCombo.Text = carGeneration;
            }
        }
    }
}
