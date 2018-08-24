using System;
using System.Collections.Generic;

namespace MarketFlowLibrary
{
    public class AssetAllocation
    {
        public string Code { get; set; }
        public string AssetDescription { get; set; }
        public decimal Weight { get; set; }
        public double Balance { get; set; }

    }
}
