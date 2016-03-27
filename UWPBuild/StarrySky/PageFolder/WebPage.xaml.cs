using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace StarrySky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebPage : Page
    {
        public static WebView WebMainView
        {
            private set;
            get;
        }

        public WebPage()
        {
            this.InitializeComponent();
            WebMainView = MainWebView;
            MainWebView.NavigationStarting += MainWebView_NavigationStarting;
            MainWebView.DOMContentLoaded += MainWebView_DOMContentLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string uri = e.Parameter.ToString();
            MainWebView.Navigate(new Uri(uri));
            base.OnNavigatedTo(e);
        }
        private void MainWebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            Library.DebugOutput("NavigationCompleted");
            WebInfo.Visibility = Visibility.Collapsed;
        }

        private void MainWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            Library.DebugOutput("NavigationStarting");
            WebInfo.Visibility = Visibility.Visible;
        }

    }
}
