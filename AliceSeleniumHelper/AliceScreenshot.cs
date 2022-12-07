using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceScreenshot
    {
        public static bool CaptureElementScreenShot(ChromeDriver driver, IWebElement element, string path)
        {
            try
            {
                var ssDriver = driver as ITakesScreenshot;
                Screenshot ss = ssDriver.GetScreenshot();
                var bmpSS = new Bitmap(new MemoryStream(ss.AsByteArray));

                Rectangle rect = new Rectangle(element.Location, element.Size);
                bmpSS = bmpSS.Clone(rect, bmpSS.PixelFormat);
                bmpSS.Save(path);

                return true;
            }
            catch
            {
                return false;
            }


        }
    }


}
