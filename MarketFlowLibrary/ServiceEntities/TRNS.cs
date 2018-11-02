using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlowLibrary
{
    public class TRNS
    {
        public string Account {get;set;}
        public DateTime? TransactionDate {get;set;}
        public string Fiche {get;set;}
	    public string TrnsDesc {get;set;}
	    public string DBCR {get;set;}
    	public decimal DbAmount {get;set;}
	    public decimal CrAmount {get;set;}
        public DateTime? DueDate {get;set;}
	    public string ClientCode {get;set;}
        public string ClientName {get;set;}
        public string CurrencySymbol {get;set;}
        public string AccountTypeDesc { get; set; }
    }
}
