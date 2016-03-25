using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Input;
public class AlphaBetaClass
{
    private MediaElement media;
    private List<string> infoList = new List<string>();
    private int infoLength;
    XElement xe = XElement.Load("ResourceFolder/AlphaBeta/AlphaBeta.xml");

    public AlphaBetaClass(ref MediaElement Media)
    {
        this.media = Media;
    }
    public void init(Grid targetGrid, string lettertype)
    {
        getXmlInfo(lettertype);
        generateGridChild(targetGrid);
    }
    private void getXmlInfo(string lettertype)
    {
        IEnumerable<XElement> letters = from ele in xe.Elements(lettertype) select ele;
        infoLength = letters.Count();
        infoList.Clear();
        Library.DebugOutput(infoLength);
        foreach (XElement ele in letters)
        {
            infoList.Add(ele.Attribute("id").Value);
        }
    }
    private void generateGridChild(Grid targetGrid)
    {
        int i = 0;
        foreach (string id in infoList)
        {
            Image img = new Image();
            string imgSource = "ms-appx:///ResourceFolder/AlphaBeta/" + id + ".jpg";
            Library.DebugOutput(imgSource);
            img.Source = new BitmapImage(new Uri(imgSource));
            img.Name = id;
            img.Tapped += Img_Tapped;
            targetGrid.Children.Add(img);
            Grid.SetRow(img, i / 6);
            Grid.SetColumn(img, i % 6);
            i++;
        }
    }

    private void Img_Tapped(object sender,TappedRoutedEventArgs e)
    {
        string id = ((Image)sender).Name;
        string mediaSource = "ms-appx:///ResourceFolder/AlphaBeta/" + id + ".mp3";
        media.Source =new Uri(mediaSource);
        media.Play();
    }
}