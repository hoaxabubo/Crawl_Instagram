using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceSource
    {
        public static string GetChromeSource(ChromeDriver chrome)
        {
            int TimeStart = Environment.TickCount;
            try
            {
                string Html = chrome.ExecuteScript("return document.documentElement.innerHTML;").ToString();
                return Html;
            }
            catch 
            {

                return null;
            }
        }

        public static bool ContainText(ChromeDriver chrome, string xText, int TimeOut = 5, int ChillTime = 200)
        {
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    string Html = chrome.ExecuteScript("return document.documentElement.innerHTML;").ToString();
                    if (Html.Contains(xText))
                    {
                        Console.WriteLine("1234");
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return false;
                }
                Thread.Sleep(ChillTime);
            }
        }

        public static string RegexText(ChromeDriver chrome, string pattern, int TimeOut = 5, int ChillTime = 200)
        {
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    string Html = chrome.ExecuteScript("return document.documentElement.innerHTML;").ToString();
                    string str =  Regex.Match(Html, pattern).Groups[1].Value.ToString();
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    else
                    {
                        return str;
                    }
                }
                catch
                {
                    return null;
                }
                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return null;
                }
                Thread.Sleep(ChillTime);
            }
        }


        public static string GetText(ChromeDriver chrome, string xpath, int TimeOut = 5, int ChillTime = 200)
        {
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    var element = chrome.FindElement(By.XPath(xpath));
                    if (element != null)
                    {
                        return element.Text;
                    }
                }
                catch
                {
                    return null;
                }
                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return null;
                }
                Thread.Sleep(ChillTime);
            }
        }
    }
}
