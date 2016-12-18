using Emojo.Lib;
using Emojo.Lib.Instagram;
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

namespace Emojo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IInstagramGetter getter;

        public MainWindow()
        {
            InitializeComponent();
            getter = InterfaceFactory.GetInstagramInterface();
            
        }

        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            var loginBrowser = new LoginBrowserWindow(getter);
            loginBrowser.OnLoggedOn += LogOn;
            loginBrowser.Show();
        }

        private void LogOn() {
            var infoWindow = new FinalInfo(getter);
            infoWindow.Show();
            infoWindow.Closed += CloseWindow;
            infoWindow.OnLogout += LogOut;
            Hide();
        }

        private void LogOut(object sender) {
            ((FinalInfo)sender).Closed -= CloseWindow;
            ((FinalInfo)sender).Close();
            var loginBrowser = new LoginBrowserWindow();
            loginBrowser.Close();
            Show();
        }

        private void CloseWindow(object sender, EventArgs e) {
            Close();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

    }
}
