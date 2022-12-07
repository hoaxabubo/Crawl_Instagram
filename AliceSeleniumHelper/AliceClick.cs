using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceClick
    {
        public static AliceSeleniumHelperReturn ByJScript(ChromeDriver chrome ,string XpathValue, int TimeOut = 30)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn{
                Status = true,
                StatusText = ""
            };
            int TimeStart = Environment.TickCount;
            XpathValue = XpathValue.Replace("'", "\"");
            while (true)
            {
                string Script = "var xpath = '" + XpathValue + "';matchingElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click(); return 'Success';";
                try
                {
                    string Re = chrome.ExecuteScript(Script).ToString();
                    if (Re == "Success")
                    {
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Click Success -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch 
                {
                  
                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    aliceSeleniumHelperReturn.Status = false;
                    aliceSeleniumHelperReturn.StatusText = $"Click False -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                    return aliceSeleniumHelperReturn;
                }
                Thread.Sleep(100);
            }
           
        }

        public static AliceSeleniumHelperReturn BySelenium(ChromeDriver chrome, string XpathValue, int TimeOut = 30)
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
                    var element = chrome.FindElement(By.XPath(XpathValue));
                    if (element != null)
                    {
                        element.Click();
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Click Success -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch 
                {
                    
                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    aliceSeleniumHelperReturn.Status = false;
                    aliceSeleniumHelperReturn.StatusText = $"Click False -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                    return aliceSeleniumHelperReturn;
                }
                Thread.Sleep(100);
            }
           
        }

    }
}
