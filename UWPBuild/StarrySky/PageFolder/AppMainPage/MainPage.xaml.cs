using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace StarrySky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static Frame MainPageFrameNav
        {
            private set;
            get;
        }

        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPageFrameNav = this.Frame;
            var listNameObj = Library.ReadSetting("MainListName");
            string listName = listNameObj == null ? "Illustration" : listNameObj.ToString();
            selectMainList(listName);
            base.OnNavigatedTo(e);
        }

        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplit.IsPaneOpen = !MainSplit.IsPaneOpen;
        }

        private void ListNavigate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string tappedName = ((StackPanel)sender).Name;
            selectMainList(tappedName);

        }
        private void selectMainList(string listName)
        {
            Library.SaveSetting("MainListName", listName);
            switch (listName)
            {
                case "Illustration":
                    MainFrame.Navigate(typeof(WebPage), "ms-appx-web:///ResourceFolder/AppMainPage/introduce.html");
                    break;
                case "GameCenter":
                    MainFrame.Navigate(typeof(GameCenterPage));
                    break;
                case "ExpertEvaluation":
                    break;
                case "Forum":
                    double deviceWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                    if (deviceWidth >= 750)
                    {
                        MainFrame.Navigate(typeof(WebPage), "http://test.zhengzi.me");
                    }
                    else
                    {
                        MainFrame.Navigate(typeof(WebPage), "http://wsq.discuz.qq.com/?siteid=265389465&f=wx&fid=2");
                    }
                    break;
                case "PersonalSettings":
                    MainFrame.Navigate(typeof(SettingPage));
                    break;
                default:
                    MainFrame.Navigate(typeof(WebPage), "ms-appx-web:///ResourceFolder/MainPageIntroduce.html");
                    break;
            }
        }
    }
}
