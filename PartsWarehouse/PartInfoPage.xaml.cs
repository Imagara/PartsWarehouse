using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace PartsWarehouse
{
    public partial class PartInfoPage : Page
    {
        Parts part;
        bool newPart;
        int _model = -1;
        public PartInfoPage(int partId)
        {
            InitializeComponent();
            if (PartNum.Text.Trim() != "")
            {
                _model = Convert.ToInt32(PartNum.Text);
            }

            if (cnt.db.Parts.Select(item => item.IdPart).Contains(partId))
            {
                newPart = false;
                part = cnt.db.Parts.Where(item => item.IdPart == partId).FirstOrDefault();
                if (part.Image == null)
                    PartImage.Source = new BitmapImage(new Uri("../Resources/NotFound.png", UriKind.RelativeOrAbsolute));
                else
                    PartImage.Source = ImagesManip.NewImage(part);
                PartName.Text = part.Name;
                PartDesc.Text = part.Description;
                PartPrice.Text = part.Price.ToString();
                PartRemain.Text = part.Remain.ToString();
                PartNum.Text = part.PartNum.ToString();
                PartCar.Text = $"{part.Car.Company} {part.Car.Name}";
                PartType.Text = part.Type;
                PartIsOriginal.Text = part.Original;
                PartManufacturer.Text = part.Manufacturer;
            }
            else
            {
                newPart = true;
                DelBut.Visibility = Visibility.Collapsed;
                PartImage.Source = new BitmapImage(new Uri("../Resources/Plus.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void PartNameManufacturer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartName.Text = string.Empty;
        }

        private void PartNameManufacturer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PartName.Text.Trim() == "")
                PartName.Text = "Название детали";
        }

        private void PartDesc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartDesc.Text = string.Empty;
        }

        private void PartDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PartDesc.Text.Trim() == "")
                PartDesc.Text = "Описание";
        }
        private void PartManufacturer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartManufacturer.Text = string.Empty;
        }

        private void PartManufacturer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PartManufacturer.Text.Trim() == "")
                PartManufacturer.Text = "Производитель";
        }

        private void PartImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BitmapImage image = ImagesManip.SelectImage();
                if (image != null)
                    PartImage.Source = image;
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }

        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new СatalogPage());
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfirmationWindow confWindow = new ConfirmationWindow();
                confWindow.ShowDialog();
                if (confWindow.answer)
                {
                    cnt.db.Parts.Remove(part);
                    cnt.db.SaveChanges();
                    NavigationService.Navigate(new СatalogPage());
                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи.");
            }
        }
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            bool check = false;
            foreach (char item in PartNum.Text)
            {
                if (!char.IsDigit(item))
                {
                    check = true;
                }
            }
            if (check)
                new ErrorWindow("В поле модель можно ввести только цифры").ShowDialog();
            else if (cnt.db.UserCar.Select(item => item.Vin).Contains(PartNum.Text) && _model != Convert.ToInt32(PartNum.Text))
                new ErrorWindow("Запчасть с данной моделью уже есть в системе").ShowDialog();
            if (!newPart)
            {
                try
                {
                    part.Image = ImagesManip.BitmapSourceToByteArray((BitmapSource)PartImage.Source);
                    part.Name = PartName.Text;
                    part.Remain = Convert.ToInt32(PartRemain.Text);
                    part.Price = Convert.ToDouble(PartPrice.Text);
                    part.PartNum = Convert.ToInt32(PartNum.Text);
                    part.Description = PartDesc.Text;
                    part.Manufacturer = PartManufacturer.Text;
                    string manufacturer = PartCar.Text.Substring(0, PartCar.Text.IndexOf(' ')),
                           name = PartCar.Text.Substring(PartCar.Text.IndexOf(' ') + 1, PartCar.Text.Length - PartCar.Text.IndexOf(' ') - 1);
                    part.IdCar = cnt.db.Parts.Where(item => item.Car.Company == manufacturer && item.Car.Name == name).Select(item => item.IdCar).FirstOrDefault();
                    part.Type = PartType.Text;
                    part.Original = PartIsOriginal.Text;
                    cnt.db.SaveChanges();
                    NavigationService.Navigate(new СatalogPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    string manufacturer = PartCar.Text.Substring(0, PartCar.Text.IndexOf(' ')),
                           name = PartCar.Text.Substring(PartCar.Text.IndexOf(' ') + 1, PartCar.Text.Length - PartCar.Text.IndexOf(' ') - 1);
                    Parts part = new Parts()
                    {
                        IdPart = cnt.db.Parts.Select(p => p.IdPart).DefaultIfEmpty(0).Max() + 1,
                        IdCar = cnt.db.Parts.Where(item => item.Car.Company == manufacturer && item.Car.Name == name).Select(item => item.Car.IdCar).FirstOrDefault(),
                        Type = PartType.Text,
                        Name = PartName.Text,
                        Manufacturer = PartManufacturer.Text,
                        Description = PartDesc.Text,
                        Image = PartImage.Source != null ? ImagesManip.BitmapSourceToByteArray((BitmapSource)PartImage.Source) : null,
                        Remain = Convert.ToInt32(PartRemain.Text),
                        Price = Convert.ToDouble(PartPrice.Text),
                        PartNum = Convert.ToInt32(PartNum.Text),
                        Original = PartIsOriginal.Text
                    };
                    cnt.db.Parts.Add(part);
                    cnt.db.SaveChanges();
                    NavigationService.Navigate(new СatalogPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void PartRemain_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartRemain.Text = string.Empty;
        }

        private void PartPrice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartPrice.Text = string.Empty;
        }

        private void PartNum_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartNum.Text = string.Empty;
        }

        private void PartCar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartCar.Text = string.Empty;
        }

        private void PartType_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartType.Text = string.Empty;
        }

        private void PartIsOriginal_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PartIsOriginal.Text = string.Empty;
        }
    }

}