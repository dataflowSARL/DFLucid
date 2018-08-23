using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class Operations
    {        
        public string refer {get;set;}
        public DateTime transDate { get; set; }
        public string typBS { get; set; }
        public string descr { get; set; }
        public DateTime? datIssue { get; set; }
        public DateTime? datMaturity { get; set; }
        public double qty { get; set; }
        public string devSymb { get; set; }
        public double openPrice { get; set; }
        public double netAmount { get; set; }
    }
}
