using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class Position
    {
        public int RowOrder {get;set;}
        public int Onhold  {get;set;}
        public string SecurityName  {get;set;}
        public DateTime? DateIssue { get; set; }
        public DateTime? DateMaturity { get; set; }
        public double Quantity  {get;set;}
        public string CurrencySymbol { get; set; }
        public double Price  {get;set;}
        public double AveragePrice { get; set; }
        public double AccruedInterest { get; set; }
        public double Balance { get; set; }
        public double BalanceSystem { get; set; }
        public double NumberOfUnits { get; set; }
        public double UnrealizedPnl { get; set; }
        public int SecurityType  {get;set;}
        public string SecurityTypeDesciption { get; set; }
        public string AssetCode  {get;set;}
        public string AssetDescription  {get;set;}
        public int AssetGroup  {get;set;}
        public decimal Weight  {get;set;}
        public double CostValueUSD  {get;set;}
        public double GainLoss  {get;set;}
        public double UnrealizedPnlUSD  {get;set;}
        public string ISIN  {get;set;}
        public string PriceType  {get;set;}
        public string SecurityCode {get; set;}
    }
}
