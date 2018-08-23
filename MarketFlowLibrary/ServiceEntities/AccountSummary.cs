using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class AccountSummary
    {
        public string account { get; set; }
        public string accType { get; set; }
        public int accSeq { get; set; }
        public string accTypeDesc { get; set; }
        public string devSymb { get; set; }
        public decimal amount { get; set; }
        public double amountSys { get; set; }
        public string accDesc { get; set; }
        public int onHold { get; set; }
    }
}
