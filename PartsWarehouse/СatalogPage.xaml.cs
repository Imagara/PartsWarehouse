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
    /// <summary>
    /// Логика взаимодействия для СatalogPage.xaml
    /// </summary>
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
        }

        private void CarCompanyBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarCompanyBox.Text = string.Empty;
        }

        private void CarCompanyBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarCompanyBox.Text == "")
                CarCompanyBox.Text = "Марка";
        }

        private void CarNameBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarNameBox.Text = string.Empty;
        }

        private void CarNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarNameBox.Text == "")
                CarNameBox.Text = "Название";
        }

        private void CarGenerationBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarGenerationBox.Text = string.Empty;
        }

        private void CarGenerationBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarGenerationBox.Text == "")
                CarGenerationBox.Text = "Поколение";
        }

        private void PartTypeBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PartTypeBox.Text = string.Empty;
        }

        private void PartTypeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PartTypeBox.Text == "")
                PartTypeBox.Text = "Тип запчасти";
        }

        private void NameBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameBox.Text = string.Empty;
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
                NameBox.Text = "Название запчасти";
        }

        private void ModelBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ModelBox.Text = string.Empty;
        }

        private void ModelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ModelBox.Text == "")
                ModelBox.Text = "Модель";
        }

        private void OriginalBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OriginalBox.Text = string.Empty;
        }

        private void OriginalBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OriginalBox.Text == "")
                OriginalBox.Text = "Оригинал: Не важно";
        }

        private void FindPartButton_Click(object sender, RoutedEventArgs e)
        {
            PartsUpdate();
        }
        private void PartsUpdate()
        {
            try
            {
                PartsListBox.Items.Clear();
                var list = cnt.db.Parts.ToList();
                if (CarCompanyBox.Text != "Марка")
                    list = list.Where(item => item.Car.Company == CarCompanyBox.Text).ToList();
                if (CarNameBox.Text != "Название")
                    list = list.Where(item => item.Car.Name == CarNameBox.Text).ToList();
                if (CarGenerationBox.Text != "Поколение")
                    list = list.Where(item => item.Car.Generation == Convert.ToInt32(CarGenerationBox.Text)).ToList();
                if (PartTypeBox.Text != "Тип запчасти")
                    list = list.Where(item => item.Type == PartTypeBox.Text).ToList();
                if (NameBox.Text != "Название запчасти")
                    list = list.Where(item => item.Name == NameBox.Text).ToList();
                if (ModelBox.Text != "Модель")
                    list = list.Where(item => item.PartNum == Convert.ToInt32(ModelBox.Text)).ToList();
                if (OriginalBox.Text != "Оригинал: Не важно")
                    list = list.Where(item => item.Original == OriginalBox.Text).ToList();
                foreach (Parts part in list)
                {
                    BitmapImage img = new BitmapImage();
                    if (part.Image != null)
                        img = new BitmapImage(new Uri("../Resources/NotFound.png", UriKind.RelativeOrAbsolute));
                    //else
                    //    img = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == idAuthor).FirstOrDefault());
                    AddPart(part.Name, part.Description, part.PartNum, img);
                }
            }
            catch
            {
                new ErrorWindow("биба").ShowDialog();
            }

        }
        private void AddPart(string name, string desc, int partNum, BitmapImage imageSource)
        {
            Grid partGrid = new Grid();
            partGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B));
            partGrid.Height = 45;
            partGrid.Width = 590;
            partGrid.Margin = new Thickness(10, 5, 10, 5);

            Image partImage = new Image();
            partImage.Source = imageSource;
            partImage.Width = 35;
            partImage.Height = 35;
            partImage.Margin = new Thickness(5);
            partImage.HorizontalAlignment = HorizontalAlignment.Left;
            partGrid.Children.Add(partImage);

            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            Label partNameLabel = new Label();
            partNameLabel.Content = name;
            partNameLabel.Foreground = Brushes.White;
            partNameLabel.FontWeight = FontWeights.Bold;
            partNameLabel.HorizontalAlignment = HorizontalAlignment.Left;
            partNameLabel.VerticalAlignment = VerticalAlignment.Top;
            partNameLabel.Margin = new Thickness(40, 0, 0, 0);

            Label dateLabel = new Label();
            dateLabel.Content = partNum.ToString();
            dateLabel.Foreground = Brushes.White;

            stackpanel.Children.Add(partNameLabel);
            stackpanel.Children.Add(dateLabel);
            partGrid.Children.Add(stackpanel);

            Label descLabel = new Label();
            descLabel.Content = desc;
            descLabel.Foreground = Brushes.White;
            descLabel.HorizontalAlignment = HorizontalAlignment.Left;
            descLabel.VerticalAlignment = VerticalAlignment.Bottom;
            descLabel.Margin = new Thickness(40, 0, 0, 0);
            partGrid.Children.Add(descLabel);

            PartsListBox.Items.Add(partGrid);
        }
    }
}
