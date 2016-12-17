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
using Emojo.Lib;
using Emojo.Lib.Instagram;

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
        private Dictionary<Emotions, SolidColorBrush> colorsDict = new Dictionary<Emotions, SolidColorBrush>();
        private Repository repository;
        private ImageDownloader downloader;

        public FinalInfo(IInstagramGetter getter)
        {
            InitializeComponent();
            repository = new Repository(getter, InterfaceFactory.GetEmotionsInterface());
            downloader = new ImageDownloader();

            colorsDict[Emotions.Anger] = new SolidColorBrush { Color = Color.FromRgb(116, 101, 218) };
            colorsDict[Emotions.Happiness] = new SolidColorBrush { Color = Color.FromRgb(197, 42, 178) };
            colorsDict[Emotions.Fear] = new SolidColorBrush { Color = Color.FromRgb(212, 65, 127) };
            colorsDict[Emotions.Sadness] = new SolidColorBrush { Color = Color.FromRgb(242, 145, 63) };
            colorsDict[Emotions.Surprise] = new SolidColorBrush { Color = Color.FromRgb(255, 220, 126) };

            ProfilePic.Fill = new ImageBrush(new BitmapImage(new Uri(repository.User.ProfilePhoto)));
            UserName.Text = repository.User.UserName;

            
            ChosenPicture = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Fear]
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Sadness]
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Surprise]
                }
            };
            Overall_People = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Alone",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "With Other People",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "No people",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Fear]
                }

            };

            DataContext = this;

        }

        private async void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            await repository.LoadUserPhotosAsync();
            int counter = 0;
            foreach (var photo in repository.Photos) {
                var image = downloader.DownloadImageTaskAsync(photo.LinkThumbnail);
                Image Box = new Image();
                Box.Source = await image;
                gridPics.Children.Add(Box);
                Grid.SetRow(Box, counter / 3);
                Grid.SetColumn(Box, counter % 3);
                Box.MouseLeftButtonDown += async (s, a) => await ChoosePicture(photo);
                counter++;
            }
            var averages = repository.GetEmotionDictionary();
            Overall = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(averages[Emotions.Anger]) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(averages[Emotions.Happiness]) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(averages[Emotions.Fear]) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Fear]
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(averages[Emotions.Sadness]) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Sadness]
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(averages[Emotions.Surprise]) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Surprise]
                }
            };


            chartOverall.Visibility = Visibility.Visible;
            chartOverallPeople.Visibility = Visibility.Visible;
            chartChosenPicture.Visibility = Visibility.Visible;
            await ChoosePicture(repository.Photos.First());

            DataContext = this;
        }

        private async Task ChoosePicture(Photo photo) {
            ChosenPic.Source = await downloader.DownloadImageTaskAsync(photo.LinkStandard);
            ChosenPicture = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(photo.Anger*100) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(photo.Happiness*100) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(photo.Fear*100) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Fear]
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(photo.Sadness*100) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Sadness]
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(photo.Surprise*100) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Surprise]
                }
            };
            DataContext = this;
        }

    }
}
