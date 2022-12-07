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
    public class AliceSend
    {
        public static AliceSeleniumHelperReturn ByJScript(ChromeDriver chrome, string XpathValue, string SendValue, int TimeOut = 30)
        {
            AliceSeleniumHelperReturn aliceSeleniumHelperReturn = new AliceSeleniumHelperReturn
            {
                Status = true,
                StatusText = ""
            };
            int TimeStart = Environment.TickCount;
            XpathValue = XpathValue.Replace("'", "\"");
            while (true)
            {
                string Script = "var xpath = '" + XpathValue + "';matchingElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.value ='" + SendValue + "'; return 'Success';";
                try
                {
                    string Re = chrome.ExecuteScript(Script).ToString();
                    if (Re == "Success")
                    {
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Send Success -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch 
                {
                   
                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    aliceSeleniumHelperReturn.Status = false;
                    aliceSeleniumHelperReturn.StatusText = $"Send False -> {XpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                    return aliceSeleniumHelperReturn;
                }
                Thread.Sleep(100);
            }
           
        }

        public static AliceSeleniumHelperReturn BySelenium(ChromeDriver chrome, string xpathValue, string sendValue, int TimeOut = 30)
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
                    var element = chrome.FindElement(By.XPath(xpathValue));
                    if (element != null)
                    {
                        element.SendKeys(sendValue);
                        aliceSeleniumHelperReturn.Status = true;
                        aliceSeleniumHelperReturn.StatusText = $"Send Success -> {xpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                        return aliceSeleniumHelperReturn;
                    }
                }
                catch 
                {
                   
                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    aliceSeleniumHelperReturn.Status = false;
                    aliceSeleniumHelperReturn.StatusText = $"Send False -> {xpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.";
                    return aliceSeleniumHelperReturn;
                }
                Thread.Sleep(100);
            }
           
        }

        public static AliceSeleniumHelperReturn BySeleniumSlowTyping(ChromeDriver chrome, string xpathValue, string sendValue, int TimeOut = 100)
        {
            if (sendValue == "")
            {
                return AliceSend.BySelenium(chrome, xpathValue, sendValue, 1);
            }

            int TimeStart = Environment.TickCount;
            while (true)
            {
                try
                {
                    var element = chrome.FindElement(By.XPath(xpathValue));
                    if (element != null)
                    {
                        int typingSecond = 3000;
                        int typingDelay = typingSecond / sendValue.Length;
                        Console.WriteLine(typingDelay);
                        for (int i = 0; i < sendValue.Length; i++)
                        {
                            element.SendKeys(sendValue[i].ToString());
                            Thread.Sleep(typingDelay);
                        }

                        return new AliceSeleniumHelperReturn(true, $"Send Success -> {xpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.");
                    }

                   
                }
                catch
                {
                    Console.WriteLine($"BySeleniumSlowTyping Fail -> {xpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.");
                }

                if (Environment.TickCount - TimeStart > TimeOut * 1000)
                {
                    return new AliceSeleniumHelperReturn(false, $"Send Fail -> {xpathValue} ~ Time: {Environment.TickCount - TimeStart} ms.");
                }
                Thread.Sleep(100);
            }

        }
    }
}
