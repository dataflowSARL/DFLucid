using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class ClosedOperations
    {
        public int RowOrder { get; set; }
        public string CurrencySymbol { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string CloseBS { get; set; }
        public DateTime? OpenedDate { get; set; }
        public string OpenBS { get; set; }
        public decimal QuantityClosed { get; set; }
        public string SecurityName { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double EstimatedProfitLoss { get; set; }
        public double EstimatedProfitLossSystem { get; set; }
    }
}
