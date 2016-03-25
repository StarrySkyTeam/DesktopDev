using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using Windows.UI.Xaml;
using Windows.Storage.Pickers;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;

class NurseryRhythmClass
{
    public List<RhythmListModel> RhythmListModels = new List<RhythmListModel>();
    public class RhythmListModel
    {
        public string id;
        public string name;
        public string pic;
        public List<SectionModel> Sections;
        public class SectionModel
        {
            public string id;
            public string content;
            public string sound;
        }
    }

    private int dataNum;
    private int nowNum = 0;
    private List<int> secNum = new List<int>();
    private int nowSec = 0;

    DispatcherTimer timer = new DispatcherTimer();

    private MediaElement play;
    private TextBlock lyric;

    public void getXmlInfo()
    {
        XElement xe = XElement.Load("ResourceFolder/NurseryRhythm/rhythm.xml");
        IEnumerable<XElement> rhythms = from ele in xe.Elements("rhythm") select ele;
        foreach (XElement ele in rhythms)
        {
            RhythmListModel rhy = new RhythmListModel();
            List<RhythmListModel.SectionModel> rs = new List<RhythmListModel.SectionModel>();
            rhy.id = ele.Attribute("id").Value;
            rhy.name = ele.Element("name").Value;
            rhy.pic = ele.Element("pic").Value;
            IEnumerable<XElement> sections = from sec in ele.Elements("section") select sec;
            foreach (XElement sec in sections)
            {
                RhythmListModel.SectionModel section = new RhythmListModel.SectionModel();
                section.id = sec.Attribute("id").Value;
                section.content = sec.Element("content").Value;
                section.sound = sec.Element("sound").Value;
                rs.Add(section);
            }
            rhy.Sections = rs;
            RhythmListModels.Add(rhy);
        }
        dataNum = RhythmListModels.Count() - 1;
        for (int i = 0; i <= dataNum; i++)
        {
            secNum.Add(RhythmListModels[i].Sections.Count());
            Library.DebugOutput(secNum[i]);
        }

    }
    public void rhythmStart(ref TextBlock title, ref Button previous, ref Button next, ref TextBlock lyric, ref Image img, ref MediaElement play)
    {
    }
    public void playOne(ref TextBlock title, ref Button previous, ref Button next, ref TextBlock lyric, ref Image img, ref MediaElement play, ref Button start)
    {
        this.play = play;
        this.lyric = lyric;
        if (nowNum == 0)
        {
            previous.Visibility = Visibility.Collapsed;
            //Library.DebugOutput("nowNum=0");
        }
        else
        {
            previous.Visibility = Visibility.Visible;
        }
        if (nowNum == dataNum)
        {
            next.Visibility = Visibility.Collapsed;
            //Library.DebugOutput("nowNum=dataNum");
        }
        else
        {
            next.Visibility = Visibility.Visible;
        }
        if (nowSec == 0)
        {
            title.Text = "三字儿歌——" + RhythmListModels[nowNum].name;
            string imgSource = "ms-appx:///ResourceFolder/NurseryRhythm/" + RhythmListModels[nowNum].pic;
            img.Source = new BitmapImage(new Uri(imgSource));
            //Library.DebugOutput("nowSec=0");
        }
        if (nowSec == secNum[nowNum])
        {
            nowSec = 0;
            lyric.Text = "";
            //Library.DebugOutput("nowSec == secNum[nowNum]");
        }
        else
        {
            timer.Interval = TimeSpan.FromMilliseconds(5000);
            //Library.DebugOutput(timer.Interval);

            //timer.Tick += (object sender, object e) =>
            //{
            //    delayPlay();
            //    Library.DebugOutput("delayplay()");
            //};
            //timer.Start();

            lyric.Text = RhythmListModels[nowNum].Sections[nowSec].content;
            string playSource = "ms-appx:///ResourceFolder/NurseryRhythm/" + RhythmListModels[nowNum].Sections[nowSec].sound;
            play.Source = new Uri(playSource);

            //Library.DebugOutput(Library.var_dump(play.NaturalDuration));
            nowSec++;
            //Library.DebugOutput("nowSec == secNum[nowNum]else");
        }

        if (nowNum == dataNum && nowSec == secNum[nowNum])
        {
            nowNum = 0;
            start.Visibility = Visibility.Visible;
            start.Content = "重新开始";
            //Library.DebugOutput("nowNum == dataNum && nowSec == secNum[nowNum]");
        }
    }
    public void delayPlay()
    {
        lyric.Text = RhythmListModels[nowNum].Sections[nowSec - 1].content;
        string playSource = "ms-appx:///ResourcesFolder/NurseryRhythm/" + RhythmListModels[nowNum].Sections[nowSec - 1].sound;
        play.Source = new Uri(playSource);
        play.Play();
        //Library.DebugOutput("delayPlay");
        timer.Stop();
    }
    public void playNext(ref TextBlock title, ref Button previous, ref Button next, ref TextBlock lyric, ref Image img, ref MediaElement play, ref Button start)
    {
        nowNum++;
        nowSec = 0;
        playOne(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref start);
    }
    public void playPrevious(ref TextBlock title, ref Button previous, ref Button next, ref TextBlock lyric, ref Image img, ref MediaElement play, ref Button start)
    {
        nowNum--;
        nowSec = 0;
        playOne(ref title, ref previous, ref next, ref lyric, ref img, ref play, ref start);
    }
    public void Refresh(ref TextBlock title, ref Button previous, ref Button next, ref TextBlock lyric, ref Image img, ref MediaElement play, ref Button start,ref Border border)
    {
        img.Source = null;
        lyric.Text = "";
        play.Stop();
        title.Text = "三字儿歌";
        nowSec = 0;
        nowNum = 0;
        img.Visibility = Visibility.Visible;
        start.Visibility = Visibility.Visible;
        border.Visibility = Visibility.Visible;
        previous.Visibility = Visibility.Collapsed;
        next.Visibility = Visibility.Collapsed;
    }


}