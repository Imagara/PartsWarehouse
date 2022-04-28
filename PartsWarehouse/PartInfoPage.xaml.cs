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
    public partial class PartInfoPage : Page
    {
        Parts part;
        public PartInfoPage(int partId)
        {
            InitializeComponent();
            part = cnt.db.Parts.Where(item => item.IdPart == partId).FirstOrDefault();
            if (part.Image == null)
                PartImage.Source = new BitmapImage(new Uri("../Resources/NotFound.png", UriKind.RelativeOrAbsolute));
            else
                PartImage.Source = ImagesManip.NewImage(part);

        }
    }
}
