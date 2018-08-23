using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class Position
    {
        public int ord {get;set;}
        public int Onhold  {get;set;}
        public string tit_nom  {get;set;}
        public DateTime? tit_dat_emi { get; set; }
        public DateTime? tit_dat_mat { get; set; }
        public double sumQty  {get;set;}
        public string devSymb { get; set; }
        public double TitCrs  {get;set;}
        public double CrsMoyen { get; set; }
        public double IntVal { get; set; }
        public double PosBalDevTitTot { get; set; }
        public double PosBalSysTot { get; set; }
        public double titnb { get; set; }
        public double UnrealizedPnl { get; set; }
        public int TitTyp  {get;set;}
        public string titTypDesc { get; set; }
        public string Asset_Cod  {get;set;}
        public string Asset_Desc  {get;set;}
        public int AssetGrp  {get;set;}
        public decimal Weight  {get;set;}
        public double CostValueUSD  {get;set;}
        public double GainLoss  {get;set;}
        public double UnrealizedPnlUSD  {get;set;}
        public string ISIN  {get;set;}
        public string MODCOD  {get;set;}
    }
}
