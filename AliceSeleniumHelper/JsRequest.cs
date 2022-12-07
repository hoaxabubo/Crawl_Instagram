using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class JsRequest
    {

        public static string Get(ChromeDriver chrome,string link )
        {
            int TimeStart = Environment.TickCount;
            try
            {
                string FileData = File.ReadAllText("Js\\RequestGet.txt");
                FileData = FileData.Replace("xxxlink", link);
                string Html = chrome.ExecuteScript(FileData).ToString();
                Console.WriteLine(Html);
                return Html;
            }
            catch
            {
                return null;
            }
        }


    }
}
