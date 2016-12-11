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
            var uri = new Uri("https://scontent-arn2-1.cdninstagram.com/t51.2885-19/s150x150/15099481_273386176397163_8805222941563289600_a.jpg");

            Image Box = new Image();
            Box.Source = new BitmapImage(uri);
            gridPics.Children.Add(Box);
            Grid.SetRow(Box, 1);
            Grid.SetColumn(Box, 1);
            Box.MouseEnter += (s, a) => ChosenPic.Source = new BitmapImage(uri);
            


            ProfilePic.Fill = new ImageBrush(new BitmapImage(new Uri(@"\\psf\Home\Desktop\A.JPG")));
        }

        
    }
}
