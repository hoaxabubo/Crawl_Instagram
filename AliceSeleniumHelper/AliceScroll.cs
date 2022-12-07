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
    public class AliceScroll
    {
       public static AliceSeleniumHelperReturn ScrollToElement(ChromeDriver driver ,string xpath)
        {
            try
            {
                IWebElement element = driver.FindElement(By.XPath(xpath));
                if (element == null)
                {
                    return new AliceSeleniumHelperReturn(false, "Khong co xpath element");
                }
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                
            }
            catch 
            {

       
            }
            return new AliceSeleniumHelperReturn(true, "Scroll Thanh Cong");

        }

    }
}
