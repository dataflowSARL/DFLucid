using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class AccountSummary
    {
        public string Account { get; set; }
        public string AccountType { get; set; }
        public int AccountSeq { get; set; }
        public string AccountTypeDesc { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal Amount { get; set; }
        public double AmountSystem { get; set; }
        public string AccountDesc { get; set; }
        public string AccountToDisplay { get; set; }
        public int OnHold { get; set; }
    }
}
