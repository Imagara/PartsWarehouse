using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace PartsWarehouse
{
    public partial class СatalogPage : Page
    {
        public СatalogPage(string carCompany = null, string carName = null, int carGeneration = -1)
        {
            InitializeComponent();
            if (carCompany != null && carName != null && carGeneration != -1)
            {
                CarCompanyBox.Text = carCompany;
                CarNameBox.Text = carName;
                CarGenerationBox.Text = carGeneration.ToString();
            }
            #region history
            if (Session.partCarCompany != null)
                CarCompanyBox.Text = Session.partCarCompany;

            if (Session.partCarName != null)
                CarNameBox.Text = Session.partCarName;

            if (Session.partCarGeneration != null)
                CarGenerationBox.Text = Session.partCarGeneration;

            if (Session.partName != null)
                NameBox.Text = Session.partName;

            if (Session.partNum != null)
                ModelBox.Text = Session.partNum;

            if (Session.partCarCompany != null)
                CarCompanyBox.Text = Session.partCarCompany;

            if (Session.partIsOriginal != null)
                OriginalBox.Text = Session.partIsOriginal;
            #endregion
            PartsUpdate();
        }
        #region LostAndPreview
        private void CarCompanyBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarCompanyBox.Text = string.Empty;
        }

        private void CarCompanyBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarCompanyBox.Text.Trim() == "")
                CarCompanyBox.Text = "Марка";
        }

        private void CarNameBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarNameBox.Text = string.Empty;
        }

        private void CarNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarNameBox.Text.Trim() == "")
                CarNameBox.Text = "Название";
        }

        private void CarGenerationBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarGenerationBox.Text = string.Empty;
        }

        private void CarGenerationBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarGenerationBox.Text.Trim() == "")
                CarGenerationBox.Text = "Поколение";
        }

        private void PartTypeBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PartTypeBox.Text = string.Empty;
        }

        private void PartTypeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PartTypeBox.Text.Trim() == "")
                PartTypeBox.Text = "Тип запчасти";
        }

        private void NameBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameBox.Text = string.Empty;
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Trim() == "")
                NameBox.Text = "Название запчасти";
        }

        private void ModelBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ModelBox.Text = string.Empty;
        }

        private void ModelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ModelBox.Text.Trim() == "")
                ModelBox.Text = "Модель";
        }

        private void OriginalBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OriginalBox.Text = string.Empty;
        }

        private void OriginalBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OriginalBox.Text.Trim() == "")
                OriginalBox.Text = "Оригинал: Не важно";
        }

        #endregion
        private void FindPartButton_Click(object sender, RoutedEventArgs e)
        {
            PartsUpdate();
        }
        private void OpenPartInfoPage(object sender, RoutedEventArgs e)
        {
            try
            {
                int partIdFromSender = Convert.ToInt32(((Label)sender).Content.ToString());
                Parts part = cnt.db.Parts.Where(item => item.PartNum == partIdFromSender).FirstOrDefault();
                if (part != null)
                {
                    NavigationService.Navigate(new PartInfoPage(part.IdPart));
                }
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }
        }
        private void PartsUpdate()
        {
            try
            {
                PartsListBox.Items.Clear();
                var list = cnt.db.Parts.ToList();
                if (CarCompanyBox.Text != "Марка")
                {
                    list = list.Where(item => item.Car.Company == CarCompanyBox.Text).ToList();
                    Session.partCarCompany = CarCompanyBox.Text;
                }
                if (CarNameBox.Text != "Название")
                {
                    list = list.Where(item => item.Car.Name == CarNameBox.Text).ToList();
                    Session.partCarName = CarNameBox.Text;
                }
                if (CarGenerationBox.Text != "Поколение")
                {
                    list = list.Where(item => item.Car.Generation == Convert.ToInt32(CarGenerationBox.Text)).ToList();
                    Session.partCarGeneration = CarGenerationBox.Text;
                }
                if (PartTypeBox.Text != "Тип запчасти")
                {
                    list = list.Where(item => item.Type == PartTypeBox.Text).ToList();
                    Session.partType = PartTypeBox.Text;
                }
                if (NameBox.Text != "Название запчасти")
                {
                    list = list.Where(item => item.Name == NameBox.Text).ToList();
                    Session.partName = NameBox.Text;
                }
                if (ModelBox.Text != "Модель")
                {
                    list = list.Where(item => item.PartNum == Convert.ToInt32(ModelBox.Text)).ToList();
                    Session.partNum = ModelBox.Text;
                }
                if (OriginalBox.Text != "Оригинал: Не важно")
                {
                    list = list.Where(item => item.Original == OriginalBox.Text).ToList();
                    Session.partIsOriginal = OriginalBox.Text;
                }
                foreach (Parts part in list)
                {
                    BitmapImage img = new BitmapImage();
                    if (part.Image == null)
                        img = new BitmapImage(new Uri("../Resources/NotFound.png", UriKind.RelativeOrAbsolute));
                    else
                        img = ImagesManip.NewImage(part);
                    AddPart(part.Name, part.Description, part.PartNum, img, part.Remain, part.Price);
                }
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }

        }
        private void AddPart(string name, string desc, int partNum, BitmapImage imageSource, int remain, double price)
        {
            Grid partGrid = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B)),
                Height = 45,
                Width = 590,
                Margin = new Thickness(10, 5, 10, 5)
            };

            Image partImage = new Image
            {
                Source = imageSource,
                Width = 35,
                Height = 35,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            partGrid.Children.Add(partImage);

            StackPanel stackpanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label partNameLabel = new Label
            {
                Content = name,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(40, 0, 0, 0)
            };

            Label partNumLabel = new Label
            {
                Content = partNum.ToString(),
                Foreground = Brushes.White,
            };
            partNumLabel.MouseDown += OpenPartInfoPage;

            Label partRemainPrice = new Label
            {
                Content = $"Осталось: {price} по {remain}руб.",
                Foreground = Brushes.White,
            };

            stackpanel.Children.Add(partNameLabel);
            stackpanel.Children.Add(partNumLabel);
            stackpanel.Children.Add(partRemainPrice);
            partGrid.Children.Add(stackpanel);

            Label descLabel = new Label
            {
                Content = desc,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(40, 0, 0, 0)
            };
            partGrid.Children.Add(descLabel);

            PartsListBox.Items.Add(partGrid);
        }

        private void AddPartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PartInfoPage(cnt.db.Parts.Select(p => p.IdPart).DefaultIfEmpty(0).Max() + 1));
        }
    }
}
