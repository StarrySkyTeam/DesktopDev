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
using Windows.UI.Xaml.Media.Imaging;


//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace NurseryRhyme
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private NurseryRhythmClass Rhythm = new NurseryRhythmClass();
        private bool repeatFlag = true;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Rhythm.getXmlInfo();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Collapsed;
            border.Visibility = Visibility.Collapsed;
            img.Visibility = Visibility.Visible;
            Rhythm.playOne(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref Start);
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            Rhythm.playPrevious(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref Start);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            Rhythm.playNext(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref Start);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Rhythm.Refresh(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref Start,ref border);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StarrySky.MainPage));
        }

        private void play_MediaOpened(object sender, RoutedEventArgs e)
        {

        }

        private void play_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (repeatFlag == true)
            {
                lyric.Text += "（请跟读）";
                play.Volume = 0;
                play.Play();
            }
            else
            {
                play.Volume = 0.5;
                Rhythm.playOne(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref Start);
            }
            repeatFlag = !repeatFlag;
        }
    }
}
