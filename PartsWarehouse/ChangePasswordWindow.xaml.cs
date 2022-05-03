using System.Linq;
using System.Windows;
namespace PartsWarehouse
{
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User user = cnt.db.User.Where(item => item.IdUser == Session.userId).FirstOrDefault();
            if (Encrypt.GetHash(OldPassBox.Text) == user.Password && NewPassBox.Text.Trim() != "" && NewPassBox.Text.Length > 5)
                user.Password = Encrypt.GetHash(NewPassBox.Text);
            else
                new ErrorWindow("Произошла ошибка").ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
