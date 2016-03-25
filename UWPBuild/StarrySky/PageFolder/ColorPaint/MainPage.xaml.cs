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

namespace ColorPaint
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GameView.NavigationStarting += GameView_NavigationStarting;
            GameView.DOMContentLoaded += GameView_DOMContentLoaded;
        }

        private void GameView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            Library.DebugOutput("NavigationCompleted");
            WebInfo.Visibility = Visibility.Collapsed;
        }

        private void GameView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            Library.DebugOutput("NavigationStarting");
            WebInfo.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GameView.Navigate(new Uri("ms-appx-web:///PageFolder/ColorPaint/html/ColorPaint.html"));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StarrySky.MainPage));
        }
    }
}
