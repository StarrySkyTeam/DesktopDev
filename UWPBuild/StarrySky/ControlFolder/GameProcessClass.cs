using StarrySky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

public class GameProcessClass
{
    public class gameModel
    {
        public string id;
        public string name;
        public string mark;
        public string logo;
        public string introduce;
    }
    private static List<gameModel> gamesModel = new List<gameModel>();
    private static bool isInit = false;

    private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
    private static Frame navTo = MainPage.MainPageFrameNav;

    public GameProcessClass()
    {
        if (!isInit)
        {
            XElement xe = XElement.Load("ResourceFolder/Games/games.xml");
            IEnumerable<XElement> games = from ele in xe.Elements("game") select ele;
            foreach (XElement ele in games)
            {
                gameModel oneGame = new gameModel();
                oneGame.id = ele.Attribute("id").Value;
                oneGame.mark = ele.Element("mark").Value;
                oneGame.name = ele.Element("name").Value;
                oneGame.logo = ele.Element("logo").Value;
                oneGame.introduce = ele.Element("introduce").Value;
                gamesModel.Add(oneGame);
            }
            isInit = true;
        }

    }

    //true for navigate to game, false for navigate to introduce
    public bool NavToGameOrIntroduce(string gameName)
    {
        var gameSetting = Library.ReadSetting(gameName + "Introduce");
        if (gameSetting == null)
        {
            return false;
        }
        else
        {
            return Convert.ToBoolean(gameSetting);
        }
    }

    public static void SetNoIntroduce(string gameName)
    {
        Library.SaveSetting(gameName + "Introduce", true);
    }

    public void SetAllIntroduce()
    {
        foreach (gameModel game in gamesModel)
        {
            Library.SaveSetting(game.name + "Introduce", false);
        }
    }

    public void displayGames(ref Grid display)
    {
        int i = 0;
        display.Children.Clear();
        foreach (gameModel oneGame in gamesModel)
        {
            //StackPanel gameInfo = new StackPanel();
            //gameInfo.Orientation = Orientation.Vertical;
            Image gameLogo = new Image();
            string logoSource = "ms-appx:///ResourceFolder/Games/" + oneGame.logo + ".png";
            //Library.DebugOutput(logoSource);
            gameLogo.Source = new BitmapImage(new Uri(logoSource));
            gameLogo.HorizontalAlignment = HorizontalAlignment.Center;
            gameLogo.VerticalAlignment = VerticalAlignment.Top;
            //TextBlock gameName = new TextBlock();
            //gameName.Text = oneGame.name;
            //gameName.HorizontalAlignment = HorizontalAlignment.Center;
            //gameName.VerticalAlignment = VerticalAlignment.Bottom;
            //gameName.FontSize = 30;
            //gameInfo.Children.Add(gameLogo);
            //gameInfo.Children.Add(gameName);
            Button gameButton = new Button();
            //gameButton.Content = gameInfo;
            gameButton.Content = gameLogo;
            gameButton.Name = oneGame.id.ToString();
            gameButton.Click += GameButton_Click;
            gameButton.Background = new SolidColorBrush(Colors.White);
            display.Children.Add(gameButton);
            Grid.SetRow(gameButton, i / 3);
            Grid.SetColumn(gameButton, i % 3);
            i++;
        }
    }

    private void GameButton_Click(object sender, RoutedEventArgs e)
    {
        string gameId = ((Button)sender).Name;
        string gameName = getGameModel(gameId).name;
        if (NavToGameOrIntroduce(gameName))
        {
            SwitchToGame(gameId);
        }
        else
        {
            SwitchToIntroduce(gameId);
        }

    }

    public static void SwitchToGame(string gameId)
    {
        string gameMark = getGameModel(gameId).mark;
        switch (gameMark)
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
                navTo.Navigate(typeof(UnityGames.MainPage), gameId);
                break;
        }
    }
    public void SwitchToIntroduce(string gameId)
    {
        navTo.Navigate(typeof(GameIntroducePage), gameId);
    }

    public static string getGameIntroduce(string gameId)
    {
        string introduceUri = "ms-appx-web:///ResourceFolder/Games/" + getGameModel(gameId).introduce + ".html";
        Library.DebugOutput(introduceUri);
        return introduceUri;
    }

    public static gameModel getGameModel(string gameId)
    {
        gameModel gameModel = gamesModel[Convert.ToInt16(gameId)];
        return gameModel;
    }
}