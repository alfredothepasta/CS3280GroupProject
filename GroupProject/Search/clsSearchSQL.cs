using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GroupProject.Search
{
    internal class clsSearchSQL
    {
        /// <summary>

        /// This SQL gets all data on an invoice for a given InvoiceID.

        /// </summary>

        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>

        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;

        }
        /// <summary>

        /// This SQL gets all invoices.

        /// </summary>

        /// <returns> All invoices.</returns>
        public string SelectAllInvoices()
        {
            string sSQL = "SELECT * FROM Invoices";
            return sSQL; 
        }
        /// <summary>

        /// This SQL gets all data from invoices with given id.

        /// </summary>

        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>

        /// <returns> Invoices with selected number.</returns>
        public string SelectInvoiceData5000(string sInvoiceID = "5000")
        {
            //this should probably just ask for a string but since there is only one i defaulted it to 5000
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID; 
            return sSQL;
        }
        /// <summary>

        /// This SQL gets all data from invoices with specified ID and Date.

        /// </summary>

        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>

        /// <param name="sDate">The date for the search.</param>

        /// <returns> All invoices with matching date and number.</returns>
        public string SelectInvoiceDataByDate(string sInvoiceID = "5000", string sDate = "#4/13/2018#")
        {
            string reformatedDate = sDate; 
            ///placeholder for rearranging date 
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = " + reformatedDate;
            return sSQL; 
        }
        /// <summary>

        /// This SQL gets all data from invoice with specified date,cost, and ID.

        /// </summary>

        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>

        /// <param name="sDate">The date for the search.</param>

        /// <param name="sCost">The cost for the search.</param>

        /// <returns> All invoices.</returns>
        public string SelectInvoiceDataByDateCost(string sInvoiceID = "5000", string sDate  = "#4/13/2018#", string sCost = "120")
        {
            string reformatedDate = sDate; 
            //placeholder for rearranging date 
            string sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = " + reformatedDate + " AND TotalCost = " + sCost;
            return sSQL; 
        }
        /// <summary>

        /// This SQL gets all invoices with specified cost.

        /// </summary>

        /// <param name="sCost">The cost for the search.</param>

        /// <returns> All invoices.</returns>
        public string SelectInvoiceByCost(string sCost = "1200")
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sCost;
            return sSQL;
        }

        /// <summary>

        /// This SQL gets all invoices with specified cost and date.

        /// </summary>

        /// <param name="sCost">The cost for the search.</param>
        
        /// <param name="sDate">The date for the search.</param>

        /// <returns> All invoices.</returns>
        public string SelectInvoiceByCostAndDate(string sCost = "1300", string sDate = "#4/13/2018#")
        {
            sDate = sDate;
            //placeholder for reformatting date
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sCost + " and InvoiceDate = " + sDate;
            return sSQL;
        }

        /// <summary>

        /// This SQL gets all invoices with specified date.

        /// </summary>

        /// <param name="sDate">The date for the search.</param>

        /// <returns> All invoices.</returns>
        public string SelectInvoiceByDate(string sDate = " #4/13/2018#")
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + sDate;
            return sSQL;
        }
        /// <summary>

        /// This SQL gets all unique invoices ordered by InvoiceNum.

        /// </summary>

        /// <returns> All invoices.</returns>
        public string SelectUniqueInvoicesSortByNumber() {
            string sSQL = "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum";
            return sSQL;
        }
        /// <summary>

        /// This SQL gets all unique invoices ordered by Date.

        /// </summary>

        /// <returns> All invoices.</returns>
        public string SelectUniqueInvoicesSortByDate()
        {
            string sSQL = "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
            return sSQL;
        }
        /// <summary>

        /// This SQL gets all unique invoices ordered by Cost.

        /// </summary>

        /// <returns> All invoices.</returns>
        public string SelectUniqueInvoicesSortByCost()
        {
            string sSQL = "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
            return sSQL;
        }
    }
}
