using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class Operations
    {        
        public string ReferOpe {get;set;}
        public DateTime? TransactionDate { get; set; }
        public string BuySell { get; set; }
        public string SecurityName { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public double Quantity { get; set; }
        public string CurrencySymbol { get; set; }
        public double OpenPrice { get; set; }
        public double NetAmount { get; set; }
    }
}
