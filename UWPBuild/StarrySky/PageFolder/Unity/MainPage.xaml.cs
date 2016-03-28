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


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace UnityGames
{

    public sealed partial class MainPage : Page
    {
        string gameId;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameId=e.Parameter.ToString();
            GameName.Text =(GameProcessClass.getGameModel(gameId)).name;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StarrySky.MainPage));
        }

        private void NavToUnity(string GameMark)
        {
            switch (GameMark)
            {
                case "DrawShape":
                    break;
                case "Puzzle":
                    break;
                case "HitMouse":
                    break;
                case "MusicArrow":
                    break;
                default:
                    break;
            }
        }

    }
}
