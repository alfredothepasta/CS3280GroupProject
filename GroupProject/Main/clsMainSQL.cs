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

        public static string UpdateInvoices(int invoiceTotal, int invoiceId)
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

        public static string DeleteInvoice(int invoiceId)
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
