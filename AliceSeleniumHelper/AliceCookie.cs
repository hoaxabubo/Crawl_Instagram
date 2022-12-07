using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceCookie
    {

        public static string Get(ChromeDriver chrome)
        {
            string C = "";
            {
                var session = chrome.Manage().Cookies.AllCookies.ToArray();
                foreach (OpenQA.Selenium.Cookie cookie in session)
                {
                    C += cookie.Name + "=" + cookie.Value + ";";
                }
            }
           return C;
        }
    }
}
