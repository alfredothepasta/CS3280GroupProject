using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.DataObjects
{
    class LineItem
    {
        public int InvoiceNum { get; set; }
        public int LineItemNum { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
    }
}
