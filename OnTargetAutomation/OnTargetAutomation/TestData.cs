using System;
using System.Xml;
using System.Text;
using System.IO;

namespace OnTargetAutomation
{
    public class TestSettings
    {
        /// <summary>
        /// settings block
        /// </summary>
        public string Environment;
        //public bool   MaximizedWnd;
        //public bool   BrowserFF;
        //public bool   BrowserChrome;
        //public bool   BrowserIE;
        //public int    ImplicitlyWait;
        //public int    RestartFreq;

        public TestSettings() {
            this.Environment = GetData.getSettings("Environment");
            //this.MaximizedWnd = GetData.getSettings("MaximizedWnd").Equals(true);
            //this.BrowserFF = GetData.getSettings("BrowserFF").Equals(true);
            //this.BrowserChrome = GetData.getSettings("BrowserChrome").Equals(true);
            //this.BrowserIE = GetData.getSettings("BrowserIE").Equals(true);
            //this.ImplicitlyWait = int.Parse(GetData.getSettings("ImplicitlyWait"));
            //this.RestartFreq = int.Parse(GetData.getSettings("RestartFreq"));
        }
    }

    public class TestData {
        public string UserName;
        public string Password;
        public string UserLastName;
        public string Channel;
        public string UserFirstName;

        public TestData(string testName)
        {
            this.UserName = GetData.getData(testName, "UserName");
            this.Password = GetData.getData(testName, "Password");
            this.UserFirstName = GetData.getData(testName, "UserFirstName");
            this.UserLastName = GetData.getData(testName, "UserLastName");
            this.Channel = GetData.getData(testName, "Channel");
        }
    }

   internal static class GetData {
        internal static string getData(string objName, string settingsName)
        {
            return getValue(
                //Properties.Resources.Data,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Data.xml"), 
                objName, 
                settingsName);
        }

        internal static string getSettings(string settingsName)
        {
            return getValue(
                //Properties.Resources.Data,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Settings.xml"),
                "Settings", 
                settingsName);
        }

        private static string getValue(string fileName, string tagName, string settingsName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNode node = doc.GetElementsByTagName(tagName).Item(0);
            for (int i = 0; i <= node.ChildNodes.Count; i++) {
                XmlNode settingsNode = node.ChildNodes.Item(i);
                if (settingsNode.Name == settingsName)
                    return settingsNode.InnerText;
            }
            return string.Empty;
        }


    }
}
