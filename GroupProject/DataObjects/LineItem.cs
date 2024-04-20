using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.DataObjects
{
    /// <summary>
    /// Holds the data for a line item
    /// </summary>
    class LineItem
    {
        /// <summary>
        /// Holds the invoice number
        /// </summary>
        public int InvoiceNum { get; set; }
        /// <summary>
        /// Holds the line item number
        /// </summary>
        public int LineItemNum { get; set; }
        /// <summary>
        /// Holds the item code for the item
        /// </summary>
        public string? ItemCode { get; set; }
        /// <summary>
        /// Holds the quantity of items
        /// </summary>
        public int? Quantity { get; set; }
    }
}
