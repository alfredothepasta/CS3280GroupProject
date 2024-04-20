using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    /// <summary>
    /// holds the data for an invoice
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Holds the invoice number
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// Holds the date for the invoice
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Holds the total cost of the invoice
        /// </summary>
        public decimal TotalCost { get; set; }
    }
}
