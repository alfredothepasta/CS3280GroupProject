using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Xceed.Wpf.AvalonDock.Themes;
using System.Reflection;
using GroupProject.DataObjects;

namespace GroupProject.Main
{
    internal class ClsMainSQL
    {

        /// <summary>
        /// The sql for updating an invoice
        /// </summary>
        /// <param name="invoiceTotal"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateInvoices(decimal invoiceTotal, int invoiceId)
        {
            try
            {
                #region Method Code
                return $"UPDATE Invoices SET TotalCost = {invoiceTotal} WHERE InvoiceNum = {invoiceTotal}";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// The sql for adding a line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddLineItem(LineItem lineItem)
        {
            try
            {
                #region Method Code
                return $"INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode, Quantity) Values({lineItem.InvoiceNum}, {lineItem.LineItemNum}, '{lineItem.ItemCode}', {lineItem.Quantity})";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// the sql for creating a new invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddInvoice(Invoice invoice)
        {
            try
            {
                #region Method Code
                return $"INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#{invoice.InvoiceDate.ToShortDateString()}#, {invoice.TotalCost})";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// the sql to get invoice by id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceById(int invoiceId)
        {
            try
            {
                #region Method Code
                return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceId}";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// the sql to get items
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItems()
        {
            try
            {
                #region Method Code
                return $"select ItemCode, ItemDesc, Cost from ItemDesc";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// the sql to get display items by invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetDisplayItemsByInvoiceId(int invoiceId)
        {
            try
            {
                return $"SELECT ItemDesc.ItemCode, ItemDesc, Cost, Quantity " +
                    $"FROM lineItems " +
                    $"INNER JOIN ItemDesc ON lineItems.ItemCode = ItemDesc.ItemCode " +
                    $"WHERE InvoiceNum = {invoiceId}";
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// the sql that gets items by item and invoice id
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItemsByItemAndInvoiceId(string itemCode, int invoiceId)
        {
            try
            {
                #region Method Code
                return $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                    $"FROM LineItems, ItemDesc " +
                    $"Where LineItems.ItemCode = {itemCode} " +
                    $"And LineItems.InvoiceNum = {invoiceId}";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// Updates a line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateLineItem(LineItem lineItem)
        {
            try {
                return $"UPDATE LineItems " +
                    $"SET ItemCode = {lineItem.ItemCode}, Quantity = {lineItem.Quantity}" +
                    $"WHERE InvoiceNum = {lineItem.InvoiceNum} AND ItemCode = {lineItem.LineItemNum}";
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// Deletes all line items
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteLineItems(int invoiceId)
        {
            try
            {
                #region Method Code
                return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceId}";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// Gets the invoice most recently added
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetLatestInvoice()
        {
            try
            {
                #region Method Code
                return $"SELECT TOP 1 InvoiceNum FROM Invoices ORDER BY InvoiceNum DESC";
                #endregion
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }
    }
}
