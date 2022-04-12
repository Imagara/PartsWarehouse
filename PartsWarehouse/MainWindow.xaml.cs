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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendMessage(string nickName, string message, string date, BitmapImage imageSource)
        {
            Grid messageGrid = new Grid();
            messageGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B));
            messageGrid.Height = 45;
            messageGrid.Width = 590;
            messageGrid.Margin = new Thickness(10, 5, 10, 5);

            Image messageImage = new Image();
            messageImage.Source = imageSource;
            messageImage.Width = 35;
            messageImage.Height = 35;
            messageImage.Margin = new Thickness(5);
            messageImage.HorizontalAlignment = HorizontalAlignment.Left;
            messageGrid.Children.Add(messageImage);

            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            Label authorLabel = new Label();
            authorLabel.Content = nickName;
            authorLabel.Foreground = Brushes.White;
            authorLabel.FontWeight = FontWeights.Bold;
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            authorLabel.Margin = new Thickness(40, 0, 0, 0);

            Label dateLabel = new Label();
            dateLabel.Content = date;
            dateLabel.Foreground = Brushes.White;

            stackpanel.Children.Add(authorLabel);
            stackpanel.Children.Add(dateLabel);
            messageGrid.Children.Add(stackpanel);

            Label messageLabel = new Label();
            messageLabel.Content = message;
            messageLabel.Foreground = Brushes.White;
            messageLabel.HorizontalAlignment = HorizontalAlignment.Left;
            messageLabel.VerticalAlignment = VerticalAlignment.Bottom;
            messageLabel.Margin = new Thickness(40, 0, 0, 0);
            messageGrid.Children.Add(messageLabel);

            PartsListBox.Items.Add(messageGrid);
        }
    }
}
