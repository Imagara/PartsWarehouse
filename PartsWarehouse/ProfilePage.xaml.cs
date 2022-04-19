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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadingCars();
        }
        private void LoadingCars()
        {
            CarsListBox.Items.Clear();
            foreach (UserCar uc in cnt.db.UserCar.Where(item => item.IdUser == Session.userId).ToList())
            {
                try
                {
                    AddCar(uc.Car.Company, uc.Car.Name, uc.Car.Generation, uc.Vin);
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }


            try
            {
                Grid addCarGrid = new Grid();
                addCarGrid.Height = 35;
                addCarGrid.Width = 485;
                addCarGrid.Margin = new Thickness(10, 0, 10, 0);

                Button addCarButton = new Button();
                addCarButton.Width = 35;
                addCarButton.Height = 35;
                addCarButton.FontSize=16;
                addCarButton.Content = "+";
                addCarButton.FontWeight = FontWeights.Black;
                addCarButton.Click += AddNewCar;

                CarsListBox.Items.Add(addCarGrid);
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }

            scroll.ScrollToEnd();
        }
        private void AddNewCar(object sender, RoutedEventArgs e)
        {

        }
        private void AddCar(string company, string name, int generation, int vin)
        {
            Grid messageGrid = new Grid();
            messageGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x44, 0x46, 0x4D));
            messageGrid.Height = 35;
            messageGrid.Width = 480;
            messageGrid.Margin = new Thickness(10, 5, 10, 5);


            Label carLabel = new Label();
            carLabel.Content = company + " " + name + " " + generation + "gen." + " (vin: " + vin + ")";
            carLabel.HorizontalAlignment = HorizontalAlignment.Left;
            carLabel.Margin = new Thickness(5);

            messageGrid.Children.Add(carLabel);

            CarsListBox.Items.Add(messageGrid);
        }
    }
}
