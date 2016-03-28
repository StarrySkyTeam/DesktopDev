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
    public sealed partial class GameIntroducePage : Page
    {
        private string gameId;
        public GameIntroducePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            gameId = e.Parameter.ToString();
            GameName.Text = @"""" +(GameProcessClass.getGameModel(gameId)).name+@""" 游戏介绍";
            WebFrame.Navigate(typeof(WebPage),GameProcessClass.getGameIntroduce(gameId));
            base.OnNavigatedTo(e);
        }
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            if (noMore.IsChecked == true)
            {
                GameProcessClass.SetNoIntroduce(GameProcessClass.getGameModel(gameId).name);
            }
            GameProcessClass.SwitchToGame(gameId);
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
