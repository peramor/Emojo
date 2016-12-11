using Emojo.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace Emojo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstagramGetter getter = new InstagramGetter();

        //1694146539.41bb294.879c4e88a59d44dd960a0b32d2a64edf

        public MainWindow()
        {
            InitializeComponent();
            button1.IsEnabled = false;
        }

        private void UnlockAll() {
            button1.IsEnabled = true;
        }

        private async void button_Click(object sender, RoutedEventArgs e) {
            Process.Start(getter.GetAuthLink());
            HttpListener listener = new HttpListener();
            string prefix = "http://localhost/";
            listener.Prefixes.Add(prefix);
            listener.Start();
            var context = await listener.GetContextAsync();
            var code = context.Request.RawUrl.Substring(context.Request.RawUrl.IndexOf("code=") + 5);
            getter.GetToken(code);
            //getter.BuildToken("1694146539.41bb294.879c4e88a59d44dd960a0b32d2a64edf", 1694146539, "roman_malts","", "https://instagram.ftpe4-2.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg");
            UnlockAll();
        }


        private async void button1_Click(object sender, RoutedEventArgs e) {
            var user = await getter.GetUser();           
        }
    }
}
