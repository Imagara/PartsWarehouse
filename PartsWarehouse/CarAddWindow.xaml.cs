using System;
using System.Linq;
using System.Windows;

namespace PartsWarehouse
{
    public partial class CarAddWindow : Window
    {
        public CarAddWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            foreach (char item in GenerationBox.Text)
            {
                if (!char.IsDigit(item))
                {
                    check = true;
                }
            }
            if (check)
                new ErrorWindow("В поле поколение можно ввести только цифры").ShowDialog();
            else if(cnt.db.UserCar.Select(item => item.Vin).Contains(VinBox.Text))
                new ErrorWindow("Данный vin уже есть в системе").ShowDialog();
            else
            {
                try
                {
                    if (!cnt.db.Car.Select(item => item.Company + item.Name + item.Generation).Contains(Manufacturer.Text + Name.Text + GenerationBox.Text))
                    {
                        Car newCar = new Car()
                        {
                            IdCar = cnt.db.Car.Select(p => p.IdCar).DefaultIfEmpty(0).Max() + 1,
                            Company = Manufacturer.Text,
                            Name = Name.Text,
                            Generation = Convert.ToInt32(GenerationBox.Text)
                        };
                        cnt.db.Car.Add(newCar);
                        cnt.db.SaveChanges();
                    }

                    int gen = Convert.ToInt32(GenerationBox.Text);

                    UserCar newUserCar = new UserCar()
                    {
                        Id = cnt.db.UserCar.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                        IdUser = Session.userId,
                        IdCar = cnt.db.Car.Where(item => item.Company == Manufacturer.Text && item.Name == Name.Text && item.Generation == gen).Select(item => item.IdCar).FirstOrDefault(),
                        Vin = VinBox.Text
                    };
                    cnt.db.UserCar.Add(newUserCar);
                    cnt.db.SaveChanges();
                    new ErrorWindow("Успешная добавление").ShowDialog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //new ErrorWindow("Ошибка").ShowDialog();
                }
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Manufacturer_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Manufacturer.Text = string.Empty;
        }

        private void Manufacturer_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Name_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Name.Text = string.Empty;
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void GenerationBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GenerationBox.Text = string.Empty;
        }

        private void GenerationBox_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void VinBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VinBox.Text = string.Empty;
        }

        private void VinBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
