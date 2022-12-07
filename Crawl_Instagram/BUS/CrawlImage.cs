using AliceSeleniumHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawl_Instagram.BUS
{
    internal class CrawlImage
    {
        public static AliceSeleniumHelperReturn SignInInstagram(ChromeDriver chrome, string email, string pass)
        {
            try
            {
                chrome.Navigate().GoToUrl("https://www.instagram.com/");
                {
                    //nhap tai khoan
                    string xAttribute = "//input[@aria-label='Phone number, username, or email']";
                    string sendValue = email;
                    var re = AliceSend.BySelenium(chrome, xAttribute, sendValue);
                    if (!re.Status)
                    {
                        return new AliceSeleniumHelperReturn();
                    }
                }
                {
                    //nhap Pass
                    string xAttribute = "//input[@aria-label='Password']";
                    string sendValue = pass;
                    var re = AliceSend.BySelenium(chrome, xAttribute, sendValue);
                    if (!re.Status)
                    {
                        return new AliceSeleniumHelperReturn();
                    }
                }
                {
                    //nhap Pass
                    string xAttribute = "//div/button/div";
                    var re = AliceClick.BySelenium(chrome, xAttribute);
                    if (!re.Status)
                    {
                        return new AliceSeleniumHelperReturn();
                    }
                }
            }
            catch
            {


            }

            return new AliceSeleniumHelperReturn();
        }
        private static Random rnd = new Random();
        private static WebClient download = new WebClient();

        public static List<string> DownloadOnePost(ChromeDriver chrome, string FolderPath)
        {
            List<string> links = new List<string>();
            links.Clear();
            try
            {
                // check isLoad

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        var isLoad = chrome.FindElement(By.XPath("//*[@aria-label=\"More options\"]"));
                        if (isLoad != null)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    Thread.Sleep(1000);
                }

                // check loại post
                string postType = CheckTypePost(chrome);
                if (postType == "OneImage")
                {
                    links.AddRange(GetLinkOneImage(chrome));


                }
                if (postType == "Album")
                {
                    links.AddRange(GetLinkAlbum(chrome));
                }

               

            }
            catch
            {


            }

           return links;
        }



        public static void GotoLink(ChromeDriver chrome, string linkToCrawl)
        {
            // vào tường
            chrome.Navigate().GoToUrl(linkToCrawl);
            Thread.Sleep(1000);
            // mở 1 ảnh đầu tiên

        }

        public static bool ClickNextPost(ChromeDriver chrome)
        {
            try
            {
                // next qua bài mới
                string xpathPostNext = "//span/*[@aria-label='Next']";
                var elementPostNext = chrome.FindElement(By.XPath(xpathPostNext));
                if (elementPostNext == null)
                {

                }
                elementPostNext.Click();
                return true;
            }
            catch
            {


            }

            return false;
        }

        public static List<string> GetLinkOneImage(ChromeDriver chrome)
        {
            List<string> links = new List<string>();

            try
            {
                string xpathOneImage = "//div[@style]/img[contains(@alt,'Photo')]";
                var elementOneImage = chrome.FindElement(By.XPath(xpathOneImage));
                string imagelink = elementOneImage.GetAttribute("src");
                links.Add(imagelink);
            }
            catch
            {


            }
            return links;
        }


        public static List<string> GetLinkAlbum(ChromeDriver chrome)
        {
            List<string> links = new List<string>();
         
            try
            {
                while (true)
                {
                    string xpathAlbum = "//div[@role='dialog']//div/img[@alt and @src]";
                    var elementAlbum = chrome.FindElements(By.XPath(xpathAlbum));
                    if (elementAlbum != null)
                    {

                    }
                    foreach (var element in elementAlbum)
                    {
                        string imagelink = element.GetAttribute("src");
                        links.Add(imagelink);
                        links = links.Distinct().ToList();
                    }
                    string xpathNext = "//button[@aria-label='Next']";
                    var elementNext = chrome.FindElement(By.XPath(xpathNext));
                    Thread.Sleep(1000);


                    if (elementAlbum == null)
                    {
                        break;
                    }
                    elementNext.Click();
                }
            }
            catch
            {

                return links;
            }
            return null;
        }


        public static string CheckTypePost(ChromeDriver chrome)
        {
            try
            {
                string xpathNext = " //div[@style]//button[@aria-label= 'Next']";
                var elementNext = chrome.FindElement(By.XPath(xpathNext));
                if (elementNext != null)
                {
                    return "Album";
                }


            }
            catch
            {


            }
            try
            {

                string xpathOneImage = "//div[@style]/img[@alt and @src]";
                var elementOneImage = chrome.FindElement(By.XPath(xpathOneImage));
                if (elementOneImage != null)
                {
                    return "OneImage";
                }


            }
            catch
            {


            }
            return null;


        }
    }
}
