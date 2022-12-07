using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceDownLoad
    {
        public static bool Image(ChromeDriver chrome, string XpathValue, string SaveImagePath)
        {
            try
            {
                var base64string = chrome.ExecuteScript($@"
                                            var c = document.createElement('canvas');
                                            var xpath = '{XpathValue}';
                                            var img = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                                            var ctx = c.getContext('2d');
                                            c.height = img.naturalHeight;
                                            c.width = img.naturalWidth;
                                            ctx.drawImage(img, 0, 0, img.naturalWidth, img.naturalHeight);
                                            var base64String = c.toDataURL();
                                            return base64String;
                                            ") as string;
                var base64 = base64string.Split(',').Last();
                using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
                {
                    using (var bitmap = new Bitmap(stream))
                    {

                        string Path = SaveImagePath;
                        bitmap.Save(Path, ImageFormat.Jpeg);
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static string ImageBase64(ChromeDriver chrome, string XpathValue)
        {
            try
            {
                var base64string = chrome.ExecuteScript($@"
                                            var c = document.createElement('canvas');
                                            var xpath = '{XpathValue}';
                                            var img = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                                            var ctx = c.getContext('2d');
                                            c.height = img.naturalHeight;
                                            c.width = img.naturalWidth;
                                            ctx.drawImage(img, 0, 0, img.naturalWidth, img.naturalHeight);
                                            var base64String = c.toDataURL();
                                            return base64String;
                                            ") as string;
                var base64 = base64string.Split(',').Last();
                return base64;
            }
            catch
            {
                return null;
            }
        }

    }
}
