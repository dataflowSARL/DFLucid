using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class RiskSummary
    {
        public string OVL { get; set; }
        public string OVLMC {get;set;}
        public double LongPosition { get; set; }
        public double ShortPosition { get; set; }
        public double LongOptions { get; set; }
        public double ShortOptions { get; set; }
        public double NetBalance { get; set; }
        public double NetAssetValue { get; set; }
        public double ExcessMargin { get; set; }
        public double ExcessMarginMC { get; set; }
        public double Leverage { get; set; }
        public double LongLeverage { get; set; }
        public double ShortLeverage { get; set; }
        public double Margin { get; set; }
        public double MarginMC { get; set; }
        public double BuyingPowerLeverage { get; set; }
        public double BuyingPower { get; set; }
    }
}
