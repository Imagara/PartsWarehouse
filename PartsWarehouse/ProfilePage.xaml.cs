using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace PartsWarehouse
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadingCars();
            User user = cnt.db.User.Where(item => item.IdUser == Session.userId).FirstOrDefault();
            LoginBox.Text = user.Login;
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
                Grid addCarGrid = new Grid
                {
                    Height = 35,
                    Width = 485,
                    Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x44, 0x46, 0x4D)),
                    Margin = new Thickness(10, 0, 10, 0)
                };

                Button addCarButton = new Button
                {
                    Width = 35,
                    Height = 35,
                    FontSize = 16,
                    Content = "+",
                    FontWeight = FontWeights.Black,
                };
                addCarButton.Click += AddNewCar;

                addCarGrid.Children.Add(addCarButton);

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
            new CarAddWindow().ShowDialog();
        }
        private void AddCar(string company, string name, int generation, string vin)
        {
            Grid messageGrid = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x44, 0x46, 0x4D)),
                Height = 35,
                Width = 480,
                Margin = new Thickness(10, 5, 10, 5)
            };

            Label carLabel = new Label
            {
                Content = company + " " + name + " " + generation + "gen." + " (vin: " + vin + ")",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };

            messageGrid.Children.Add(carLabel);

            CarsListBox.Items.Add(messageGrid);
        }

        private void ChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow().ShowDialog();
        }
    }
}
