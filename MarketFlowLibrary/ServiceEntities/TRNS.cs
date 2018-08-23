using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class TRNS
    {
        public Int64 TID {get;set;}
        public string account {get;set;}
        public DateTime? transDate {get;set;}
        public int fiche {get;set;}
	    public string trnsDesc {get;set;}
	    public string dbCr {get;set;}
    	public double dbAmount {get;set;}
	    public double crAmount {get;set;}
        public DateTime? dueDate {get;set;}
	    public string client_code {get;set;}
        public string client_name {get;set;}
        public string currSymb {get;set;}
        public string accTypeDesc { get; set; }
    }
}
