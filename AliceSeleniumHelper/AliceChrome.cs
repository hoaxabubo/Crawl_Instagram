using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceChrome
    {
        public static object lockOpenChrome = new object();
        public static ChromeDriver OpenChrome()
        {
            ChromeDriver driver =null;
            try
            {

                #region Khai báo Selenium
                IJavaScriptExecutor js = null;
                ChromeOptions options = new ChromeOptions();
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
               
                options.AddArguments(new string[] {
                        "--disable-extensions",
                        "--disable-notifications",
                        $"--window-size=400,600",
                        "--no-sandbox",
                        "--blink-settings=imagesEnabled=true",
                        "--disable-gpu",// applicable to windows os only
                        "--disable-dev-shm-usage",//overcome limited resource problems       
                        "--disable-web-security",
                        "--disable-rtc-smoothness-algorithm",
                        "--disable-webrtc-hw-decoding",
                        "--disable-webrtc-hw-encoding",
                        "--disable-webrtc-multiple-routes",
                        "--disable-webrtc-hw-vp8-encoding",
                        "--enforce-webrtc-ip-permission-check",
                        "--force-webrtc-ip-handling-policy",
                        "ignore-certificate-errors",
                        "disable-infobars",
                        "mute-audio",
                        "--disable-popup-blocking"
                    });




                //options.AddArgument("--user-data-dir=" + AppDomain.CurrentDomain.BaseDirectory + "Profile");
               
                service.HideCommandPromptWindow = true;
                lock (lockOpenChrome)
                {
                    driver = new ChromeDriver(service, options, new TimeSpan(0, 1, 0));
                }
                
               
                #endregion Khai báo Selenium

                driver.Navigate().GoToUrl("https://iphey.com/");
                return driver;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return driver;
            }
            


        }
        static int findFreePort()
        {
            int port = 0;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
                socket.Bind(localEP);
                localEP = (IPEndPoint)socket.LocalEndPoint;
                port = localEP.Port;
            }
            finally
            {
                socket.Close();
            }
            return port;
        }

        public static bool isChromeRunning(ChromeDriver chrome)
        {
            try
            {
                var a = chrome.Title;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
