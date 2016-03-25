using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

class Library
{
    public static void DebugOutput(Object log)
    {
        Debug.WriteLine(log);
    }

    public static string var_dump(object info)
    {
        StringBuilder sb = new StringBuilder();

        Type t = info.GetType();
        PropertyInfo[] props = t.GetProperties();
        sb.AppendFormat("{0,-18} {1}", "Name", "Value");
        sb.AppendLine();
        foreach (PropertyInfo prop in props)
        {
            sb.AppendFormat("{0,-18} {1}", prop.Name, prop.GetValue(info, null).ToString());
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
    public static void SaveSetting(string key, Object value)
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        localSettings.Values[key] = value;
    }
    public static object ReadSetting(string key)
    {
        object value = localSettings.Values[key];
        return value;
    }

    public static void Notification(string title, string content)
    {
        ToastTemplateType toastTemplate = ToastTemplateType.ToastText04;
        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
        toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
        toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
        IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
        //((XmlElement)toastNode).SetAttribute("duration", "long");
        XmlElement audio = toastXml.CreateElement("audio");
        audio.SetAttribute("src", $"ms-winsoundevent:Notification.Default");
        ToastNotification toast = new ToastNotification(toastXml);
        ToastNotificationManager.CreateToastNotifier().Show(toast);
    }
}