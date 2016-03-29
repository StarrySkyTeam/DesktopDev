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
using Windows.ApplicationModel.Activation;
using StarrySky;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace OnlineGames
{

    public sealed partial class MainPage : Page
    {
        string gameId;
        string gameName;
        string gameMark;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameId = e.Parameter.ToString();
            gameName = (GameProcessClass.getGameModel(gameId)).name;
            gameMark = (GameProcessClass.getGameModel(gameId)).mark;

            GameName.Text = gameName;
            GamePlayer.NavigationStarting += GamePlayer_NavigationStarting;
            GamePlayer.DOMContentLoaded += GamePlayer_DOMContentLoaded;
            GamePlayer.Navigate(new Uri(getUri()));
        }

        private void GamePlayer_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            GameInfo.Visibility = Visibility.Collapsed;
        }

        private void GamePlayer_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            GameInfo.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StarrySky.MainPage));
        }

        private string getUri()
        {
            string uri = "http://debug.zhengzi.me/games/";
            uri += gameMark;
            uri += "/index.html";
            Library.DebugOutput(uri);
            return uri;
        }

        //暂时不起作用
        private async void gameFullScreen()
        {
            List<string> arg = new List<string> { "1" };
            string rev = await GamePlayer.InvokeScriptAsync("SetFullscreen", arg);
        }

    }
}
