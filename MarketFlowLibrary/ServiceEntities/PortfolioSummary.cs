﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlowLibrary
{
    public class PortfolioSummary
    {
        public int RowOrder { get; set; }
        public int ClientDepositCode { get; set; }
        public string ClientDepositaryPlace { get; set; }
        public string SecuritySubTypeCode { get; set; }
        public string SecuritySubTypeDesc{ get; set; }
        public double BalanceSystem { get; set; }
        public double TotalBalanceSystem { get; set; }
        public double WeightPerc { get; set; }
        public int OnHold { get; set; }
    }
}
