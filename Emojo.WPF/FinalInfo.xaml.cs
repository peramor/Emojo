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
        //😡 😱 😁 😭 😳
        public SeriesCollection Overall { get; set; }
        public SeriesCollection ChosenPicture { get; set; }
        public SeriesCollection Overall_People { get; set; }
        private Dictionary<Emotions, SolidColorBrush> colorsDict = new Dictionary<Emotions, SolidColorBrush>();
        private Repository repository;
        private ImageDownloader downloader;

        public delegate void Logout(object sender);
        public event Logout OnLogout;

        public FinalInfo(IInstagramGetter getter)
        {
            InitializeComponent();
            repository = new Repository(getter, InterfaceFactory.GetEmotionsInterface());
            downloader = new ImageDownloader();

            colorsDict[Emotions.Anger] = new SolidColorBrush { Color = Color.FromRgb(94, 183, 159) };
            colorsDict[Emotions.Happiness] = new SolidColorBrush { Color = Color.FromRgb(255, 217, 178) };
            colorsDict[Emotions.Fear] = new SolidColorBrush { Color = Color.FromRgb(195, 55, 65) };
            colorsDict[Emotions.Sadness] = new SolidColorBrush { Color = Color.FromRgb(72, 120, 118) };
            colorsDict[Emotions.Surprise] = new SolidColorBrush { Color = Color.FromRgb(251, 149, 71) };

            ProfilePic.Fill = new ImageBrush(new BitmapImage(new Uri(repository.User.ProfilePhoto)));
            UserName.Text = repository.User.UserName;
            FullName.Text = repository.User.FullName;
            
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

        }

        

        private async Task ChoosePicture(Photo photo) {
            ChosenPic.Source = await downloader.DownloadImageTaskAsync(photo.LinkStandard);
            ChosenPicture = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Anger",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(photo.Anger,2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(photo.Happiness,2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(photo.Fear)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Fear]
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(photo.Sadness,2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Sadness]
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(photo.Surprise,2)) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Surprise]
                }
            };
            DataContext = null;
            DataContext = this;
        }

        private async void Start_loading(object sender, RoutedEventArgs e)
        {
            textBlockLoading.Visibility = Visibility.Visible;
            await repository.LoadUserPhotosAsync();
            int counter = 0;
            foreach (var photo in repository.Photos)
            {
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
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(averages[Emotions.Anger],2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "Happiness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(averages[Emotions.Happiness],2)) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Happiness]

                },
                new PieSeries
                {
                    Title = "Fear",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(averages[Emotions.Fear],2)) },
                    DataLabels = true,
                    Fill= colorsDict[Emotions.Fear]
                },
                new PieSeries
                {
                    Title = "Sadness",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(averages[Emotions.Sadness],2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Sadness]
                },
                 new PieSeries
                {
                    Title = "Surprise",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(averages[Emotions.Surprise],2)) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Surprise]
                }
            };

            Overall_People = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Alone",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(repository.PeopleCounts[1]) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Anger]

                },
                new PieSeries
                {
                    Title = "With Other People",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(repository.PeopleCounts[2]) },
                    DataLabels = true,
                    Fill = colorsDict[Emotions.Happiness]

                },

            };

            
            chartOverall.Visibility = Visibility.Visible;
            chartOverallPeople.Visibility = Visibility.Visible;
            chartChosenPicture.Visibility = Visibility.Visible;
            OverallInfoTextBox.Visibility = Visibility.Visible;
            ChooseToAnalyzeTextBox.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Visible;
            await ChoosePicture(repository.Photos.First());
            DataContext = null;
            DataContext = this;
            textBlockLoading.Visibility = Visibility.Hidden;

        }

        public void Logout_Clicked (object sender, EventArgs e) {
            OnLogout(this);
        }
    }
}
