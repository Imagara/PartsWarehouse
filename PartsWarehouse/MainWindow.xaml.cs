using System.Linq;
using System.Windows;
using System.Windows.Input;

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
                if (cnt.db.UserCar.Select(item => item.Vin).Contains(VinBox.Text))
                {
                    Car car = cnt.db.Car.Where(item => item.IdCar == cnt.db.UserCar.Where(uc => uc.Vin == VinBox.Text).Select(uc => uc.IdCar).FirstOrDefault()).FirstOrDefault();
                    MainFrame.Content = new СatalogPage(car.Company, car.Name, car.Generation);
                    new ErrorWindow($"Найдено: ({car.Company}, {car.Name}, {car.Generation} поколения)").ShowDialog();
                }
                else if (VinBox.Text == "Vin..." || VinBox.Text.Trim() == "")
                    new ErrorWindow("Введите Vin.").ShowDialog();
                else
                    new ErrorWindow("Ничего не найдено.").ShowDialog();
            }
            catch
            {

            }

        }

        private void VinBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VinBox.Text = string.Empty;
        }

        private void VinBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (VinBox.Text.Trim() == "")
                VinBox.Text = "Vin...";
        }
    }
}
