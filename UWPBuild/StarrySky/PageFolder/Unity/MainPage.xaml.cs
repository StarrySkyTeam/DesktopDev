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
using UnityPlayer;
using Windows.ApplicationModel.Activation;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace UnityGames
{

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavToUnity(e.Parameter.ToString());
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            UnityGames.GamePage.AppCallBack.UnityPause(1);
            this.Frame.Navigate(typeof(StarrySky.MainPage));
        }

        private void NavToUnity(string GameName)
        {
            int scentNum;
            switch (GameName)
            {
                case "DrawShape":
                    scentNum = 1;
                    break;
                case "Puzzle":
                    scentNum = 2;
                    break;
                case "HitMouse":
                    scentNum = 3;
                    break;
                case "MusicArrow":
                    scentNum = 4;
                    break;
                default:
                    scentNum = -1;
                    break;
            }
            UnityGames.GamePage.AppCallBack = new AppCallbacks();
            UnityGames.GamePage.AppCallBack.SetAppArguments(scentNum.ToString());
            UnityGamePage.Navigate(typeof(UnityGames.GamePage));
        }

    }
}
