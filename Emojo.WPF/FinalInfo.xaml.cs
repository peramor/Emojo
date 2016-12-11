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
using LiveCharts.Defaults;

namespace Emojo.WPF
{
    /// <summary>
    /// Логика взаимодействия для FinalInfo.xaml
    /// </summary>
    public partial class FinalInfo : Window
    {
        public SeriesCollection Overall { get; set; }
        public SeriesCollection ChosenPicture { get; set; }
        public SeriesCollection Overall_People { get; set; }
        public FinalInfo()
        {
            InitializeComponent();
            SolidColorBrush AngerColor = new SolidColorBrush();
            AngerColor.Color = Color.FromRgb(116, 101, 218);
       
            SolidColorBrush HapinessColor = new SolidColorBrush();
            HapinessColor.Color = Color.FromRgb(197, 42, 178);
       
            SolidColorBrush FearColor = new SolidColorBrush();
            FearColor.Color = Color.FromRgb(212, 65, 127);
       
            SolidColorBrush SadnessColor = new SolidColorBrush();
            SadnessColor.Color = Color.FromRgb(242, 145, 63);
        
            SolidColorBrush SurpriseColor = new SolidColorBrush();
            SurpriseColor.Color = Color.FromRgb(255, 220, 126);

            Overall = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true,
                    Fill = AngerColor

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true,
                    Fill=HapinessColor

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true,
                    Fill=FearColor
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true,
                    Fill=SadnessColor
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true,
                    Fill=SurpriseColor
                }
            };

            ChosenPicture = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    Fill = AngerColor

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    Fill=HapinessColor

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    Fill=FearColor
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    Fill=SadnessColor
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    Fill=SurpriseColor
                }
            };
            Overall_People = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Alone",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(5) },
                    DataLabels = true,
                    Fill = AngerColor

                },
                new PieSeries
                {
                    Title = "With Other People",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(5) },
                    DataLabels = true,
                    Fill=HapinessColor

                },
                new PieSeries
                {
                    Title = "No people",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true,
                    Fill=FearColor
                }

            };


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
            Box.MouseEnter += (s, a) =>
            {
                var r = new Random();
                foreach (var series in ChosenPicture)
                {
                    foreach (var observable in series.Values.Cast<ObservableValue>())
                    {
                        observable.Value = r.Next(0, 10);
                    }
                }
            };
            


            ProfilePic.Fill = new ImageBrush(new BitmapImage(new Uri(@"\\psf\Home\Desktop\A.JPG")));
        }

        
    }
}
