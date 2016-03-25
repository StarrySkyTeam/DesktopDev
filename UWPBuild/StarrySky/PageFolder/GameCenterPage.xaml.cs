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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace StarrySky.PageFolder
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GameCenterPage : Page
    {
        public GameCenterPage()
        {
            this.InitializeComponent();
        }
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            string gameName = ((Button)sender).Name;
            Frame navTo = MainPage.MainPageFrameNav;
            Library.DebugOutput(gameName);
            switch (gameName)
            {
                case "AlphaBeta":
                    navTo.Navigate(typeof(AlphaBeta.MainPage));
                    break;
                case "NurseryRhythm":
                    navTo.Navigate(typeof(NurseryRhyme.MainPage));
                    break;
                case "ColorPaint":
                    navTo.Navigate(typeof(ColorPaint.MainPage));
                    break;
                case "DrawShape":
                case "Puzzle":
                case "HitMouse":
                case "MusicArrow":
                    navTo.Navigate(typeof(UnityGames.MainPage),gameName);
                    break;
            }

        }

        
    }


}
