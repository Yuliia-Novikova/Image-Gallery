using Image_Gallery.ViewModel;
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

namespace Image_Gallery.View
{
    /// <summary>
    /// Interaction logic for Image_Gallery.xaml
    /// </summary>
    public partial class Gallery : Window
    {
        public Gallery(Model.User user)
        {
            InitializeComponent();
            using (GalleryViewModel context = new GalleryViewModel(user))
            {
                DataContext = context;
            }
        }
    }
}
