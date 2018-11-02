using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketFlowLibrary
{
    public class MKFUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string WebCliCode { get; set; }
        public string IPAddr { get; set; }
        public string CliCode { get; set; }
    }
}