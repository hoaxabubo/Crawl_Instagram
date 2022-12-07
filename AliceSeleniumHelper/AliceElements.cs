using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceElements
    {

        public static IWebElement FindElement(ChromeDriver chrome, string XpathValue, int TimeOut = 30)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
            {
                Status = true,
                StatusText = ""
            };
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    IWebElement element = chrome.FindElement(By.XPath(XpathValue));
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch
                {

                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return null;
                }
                Thread.Sleep(100);
            }

        }

        public static ReadOnlyCollection<IWebElement> FindElements(ChromeDriver chrome, string XpathValue, int TimeOut = 30)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
            {
                Status = true,
                StatusText = ""
            };
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> elements = chrome.FindElements(By.XPath(XpathValue));
                    if (elements != null)
                    {
                        if (elements.Count != 0)
                        {
                            return elements;
                        }
                    }

                }
                catch
                {

                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return null;
                }
                Thread.Sleep(100);
            }

        }
    }
}
