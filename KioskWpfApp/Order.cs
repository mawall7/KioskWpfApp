using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskWpfApp
{
    public class Order
    {
        public string OrderName { get; set; }
        public int OrderNumber { get; set; }
        public bool IsReady { get; set; }
        public bool TakeAway { get; set; }
    }
}
