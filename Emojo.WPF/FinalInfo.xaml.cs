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
using LiveCharts;
using LiveCharts.Wpf;

namespace Emojo.WPF
{
    /// <summary>
    /// Логика взаимодействия для FinalInfo.xaml
    /// </summary>
    public partial class FinalInfo : Window
    {
        public FinalInfo()
        {
            InitializeComponent();
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
          Image Box = new Image();
            Box.Source= (new BitmapImage(new Uri(@"\\psf\Home\Desktop\A.JPG")));
            gridPics.Children.Add(Box);
           Grid.SetRow(Box, 1);
           Grid.SetColumn(Box, 1);
           ProfilePic.Fill = new ImageBrush(new BitmapImage(new Uri(@"\\psf\Home\Desktop\A.JPG")));
        }
        
    }
}
