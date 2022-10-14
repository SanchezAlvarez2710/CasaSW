using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models
{
    public class Mail
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}