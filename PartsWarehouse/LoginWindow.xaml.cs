
using System.Linq;

using System.Windows;

using System.Windows.Input;

namespace PartsWarehouse
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            LogBox.Focus();
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidLogAndPass(LogBox.Text, PassBox.Password))
                    new ErrorWindow("Поля не могут быть пустыми").Show();
                else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                    new ErrorWindow("Неверный логин или пароль").Show();
                else
                {
                    Profile.userId = cnt.db.User.Where(item => item.Login == LogBox.Text).Select(item => item.Id).FirstOrDefault();
                    new MainWindow().Show();
                    this.Close();
                }
            }
            catch
            {
                new ErrorWindow("Ошибка входа").ShowDialog();
            }
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            this.Close();
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
    }
}
