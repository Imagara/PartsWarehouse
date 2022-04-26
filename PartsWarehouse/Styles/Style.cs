using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace PartsWarehouse
{
    public partial class Style : ResourceDictionary
    {
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x2F, 0x31, 0x36));
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x36, 0x3C));
        }
    }
}