using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary
{
    public class PortSum
    {
        public int cliDepPlaceCode { get; set; }
        public string Name { get; set; }
        public string subTypeDesc{ get; set; }
        public double PosBalSysTot { get; set; }
        public double PosBalSysTotWithHold { get; set; }
        public int OnHold { get; set; }
    }
}
