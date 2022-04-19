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

namespace PartsWarehouse
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new СatalogPage();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMininize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ProfileButton(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProfilePage();
        }

        private void CatalogButton(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new СatalogPage();
        }

        private void FindWithVin(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cnt.db.UserCar.Select(item => item.Vin).Contains(Convert.ToInt32(VinBox.Text)))
                {
                    int vin = Convert.ToInt32(VinBox.Text);
                    Car car = cnt.db.Car.Where(item => item.IdCar == cnt.db.UserCar.Where(uc => uc.Vin == vin).Select(uc => uc.IdCar).FirstOrDefault()).FirstOrDefault();
                    MainFrame.Content = new СatalogPage(car.Company, car.Name, car.Generation);
                    new ErrorWindow((car.Company, car.Name, car.Generation).ToString()).ShowDialog();
                }
                else
                    new ErrorWindow("Ничего не найдено.").ShowDialog();
            }
            catch
            {

            }

        }
    }
}
