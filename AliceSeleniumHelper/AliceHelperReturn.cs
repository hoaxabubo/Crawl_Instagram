using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceSeleniumHelper
{
    public class AliceSeleniumHelperReturn
    {
        public AliceSeleniumHelperReturn()
        {

        }
        public AliceSeleniumHelperReturn(bool status,string statusText)
        {
            Status = status;
            StatusText = statusText;    
        }

        public bool Status { get; set; }
        public string StatusText { get; set; }
    }
}
