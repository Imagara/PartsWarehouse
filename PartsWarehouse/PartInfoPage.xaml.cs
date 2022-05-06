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
        public PartInfoPage(int partId)
        {
            InitializeComponent();
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
                PartImage.Source = new BitmapImage(new Uri("../Resources/Plus.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void PartNameManufacturer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PartNameManufacturer_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void PartDesc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PartDesc_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        private void PartManufacturer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PartManufacturer_LostFocus(object sender, RoutedEventArgs e)
        {

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
        private void SaveButton(object sender, RoutedEventArgs e)
        {
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
                    MessageBox.Show($"|{manufacturer}|{name}|");
                    Parts part = new Parts()
                    {
                        IdPart = cnt.db.Parts.Select(p => p.IdPart).DefaultIfEmpty(0).Max() + 1,
                        IdCar = cnt.db.Parts.Where(item => item.Car.Company == manufacturer && item.Car.Name == name).Select(item => item.IdCar).FirstOrDefault(),
                    Type = PartType.Text,
                        Name = PartName.Text,
                        Manufacturer = PartManufacturer.Text,
                        Description = PartDesc.Text,
                        //Image = PartImage.Source != null ? ImagesManip.BitmapSourceToByteArray((BitmapSource)PartImage.Source) : null,
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
    }

}
