using System.Windows;

namespace PartsWarehouse
{
    public partial class ConfirmationWindow : Window
    {
        public bool answer;
        public ConfirmationWindow()
        {
            InitializeComponent();
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
