using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class PortfolioSummary
    {
        public string ClientDepositaryPlace { get; set; }
        public string SecuritySubTypeDesc{ get; set; }
        public double BalanceSystem { get; set; }
        public double TotalBalanceSystem { get; set; }
        public double WeightPerc { get; set; }
        public int OnHold { get; set; }
    }
}
