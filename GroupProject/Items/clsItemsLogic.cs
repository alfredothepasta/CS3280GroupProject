using GroupProject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    internal class clsItemsLogic
    {
        //GetAllItems returns List<clsItem>
        //AddItem(clsItem)
        //EditItem(clsItem clsOldItem, clsItem clsNewItem)
        //DeleteItem(clsItem clsItemToDelete)
        //IsItemOnInvoice(clsItem)

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        public List<clsItem> GetAllItems()
        {
            try
            {
                List<clsItem> items = new List<clsItem>(); // Create a list to store items
                string sSQL = new clsItemsSQL().GetAllItems(); // Retrieve SQL query to get all items

                // Execute SQL statement and get DataSet
                int rowCount = 0;
                DataSet ds = new clsDataAccess().ExecuteSQLStatement(sSQL, ref rowCount);

                // Loop through the DataTable and create clsItem objects
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    clsItem item = new clsItem();
                    item.Code = row["ItemCode"].ToString();
                    item.Description = row["ItemDesc"].ToString();
                    item.Cost = Convert.ToDecimal(row["Cost"]);

                    // Add the item to the list
                    items.Add(item);
                }

                return items; // Return the list of items
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Update item description and cost
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="newDescription"></param>
        /// <param name="newCost"></param>
        public void UpdateItem(string itemCode, string newDescription, decimal newCost)
        {
            try
            {
                string sSQL = new clsItemsSQL().UpdateItem(itemCode, newDescription, newCost);
                int rowCount = 0; // Initialize rowCount
                new clsDataAccess().ExecuteSQLStatement(sSQL, ref rowCount); // Pass rowCount
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Insert new item
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="description"></param>
        /// <param name="cost"></param>
        public void InsertItem(string itemCode, string description, decimal cost)
        {
            try
            {
                string sSQL = new clsItemsSQL().InsertItem(itemCode, description, cost);
                int rowCount = 0;
                new clsDataAccess().ExecuteSQLStatement(sSQL, ref rowCount);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="itemCode"></param>
        public void DeleteItem(string itemCode)
        {
            try
            {
                string sSQL = new clsItemsSQL().DeleteItem(itemCode);
                int rowCount = 0;
                new clsDataAccess().ExecuteSQLStatement(sSQL, ref rowCount);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Check if item is on an invoice
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool IsItemOnInvoice(string itemCode)
        {
            try
            {
                string sSQL = new clsItemsSQL().GetDistinctInvoices(itemCode);
                int rowCount = 0;
                DataSet ds = new clsDataAccess().ExecuteSQLStatement(sSQL, ref rowCount);

                return rowCount > 0; // Return true if item is on an invoice
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
