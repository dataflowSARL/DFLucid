using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class ClosedOperations
    {
        public int ord { get; set; }
        public string devSymb { get; set; }
        public DateTime closeDate { get; set; }
        public string closeBS { get; set; }
        public DateTime openDate { get; set; }
        public string openBS { get; set; }
        public decimal qtyClos { get; set; }
        public string descr { get; set; }
        public double openPrice { get; set; }
        public double closePrice { get; set; }
        public double estimatedPnL { get; set; }
    }
}
