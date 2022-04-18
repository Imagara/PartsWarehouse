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
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidLength(NickNameBox.Text.Trim()))
                    new ErrorWindow("Поле «Логин» должно содержать не менее 5 символов.").Show();
                else if (!Functions.IsValidLength(PassBox.Password.Trim()))
                    new ErrorWindow("Поле «Пароль» должно содержать не менее 5 символов.").Show();
                else if (!Functions.IsLogEqualPass(NickNameBox.Text, PassBox.Password))
                    new ErrorWindow(" Поля «Логин» и «Пароль» не должны быть равны").Show();
                else if (Functions.IsLoginAlreadyTaken(NickNameBox.Text))
                    new ErrorWindow("Данный логин уже занят").Show();
                else
                {
                    User newUser = new User()
                    {
                        IdUser = cnt.db.User.Select(p => p.IdUser).DefaultIfEmpty(0).Max() + 1,
                        Login = NickNameBox.Text,
                        Password = Encrypt.GetHash(PassBox.Password),
                    };
                    cnt.db.User.Add(newUser);
                    cnt.db.SaveChanges(); ;
                    new ErrorWindow("Успешная регистрация").ShowDialog();
                    new LoginWindow().Show();
                    this.Close();
                }
            }
            catch
            {
                new ErrorWindow("Ошибка.").ShowDialog();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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