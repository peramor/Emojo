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
using System.Windows.Shapes;

namespace Emojo.WPF {
    /// <summary>
    /// Interaction logic for LoginBrowserWindow.xaml
    /// </summary>
    public partial class LoginBrowserWindow : Window {
        IInstagramGetter getter;

        public delegate void LoggedOn();
        public event LoggedOn OnLoggedOn;

        public LoginBrowserWindow(IInstagramGetter getter) {
            InitializeComponent();
            this.getter = getter;
            browser.Navigate(getter.GetAuthLink());
            
        }

        private async void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            var url = e.Uri.Query;
            if (url.Contains("code")) {
                Hide();
                var code = url.Substring(url.IndexOf("code") + 5);
                await getter.GetToken(code);               
                OnLoggedOn();
                Close();                
            }
        }
    }
}
