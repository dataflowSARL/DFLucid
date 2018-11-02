using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketFlowLibrary
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string WebCliCode { get; set; }
        public string CliID { get; set; }
        public string CliCode { get; set; }
        public string WebMessage { get; set; }

    }
}