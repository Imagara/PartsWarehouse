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
using System.Windows.Shapes;

namespace PartsWarehouse
{
    /// <summary>
    /// Логика взаимодействия для ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public bool answer;
        public ConfirmationWindow(bool _answer = false)
        {
            InitializeComponent();
            answer = _answer;
        }
        private void YesButton(object sender, RoutedEventArgs e)
        {
            answer = true;
            this.Close();
        }
        private void NoButton(object sender, RoutedEventArgs e)
        {
            answer = false;
            this.Close();
        }
    }
}
