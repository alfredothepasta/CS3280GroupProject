using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    internal class clsItemsSQL
    {
        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <returns></returns>
        public string GetAllItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns distinct invoice numbers for a given item code.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetDistinctInvoices(string itemCode)
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = '" + itemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates item description and cost for a specific item code.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="newDescription"></param>
        /// <param name="newCost"></param>
        /// <returns></returns>
        public string UpdateItem(string itemCode, string newDescription, decimal newCost)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET ItemDesc = '" + newDescription + "', Cost = " + newCost + " WHERE ItemCode = '" + itemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Inserts a new item into the database.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="description"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string InsertItem(string itemCode, string description, decimal cost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('" + itemCode + "', '" + description + "', " + cost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an item based on item code.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string DeleteItem(string itemCode)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
