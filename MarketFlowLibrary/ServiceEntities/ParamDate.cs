using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlowLibrary
{
    public class ParamDate
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public MKFUser userMKF  { get; set; }
        public string FromAcc { get; set; }
        public string ToAcc { get; set; }
    }
}
