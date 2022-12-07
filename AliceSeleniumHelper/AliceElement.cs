using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceElement
    {
        public static IWebElement WaitElement (ChromeDriver driver, string xpath, int time)
        {
            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    var element = driver.FindElement(By.XPath(xpath));
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch
                {
                }

                if (Environment.TickCount - TimeStart > time * 1000)
                {
                    return null;
                }
                Thread.Sleep(100);
            }
        }
    }
}
