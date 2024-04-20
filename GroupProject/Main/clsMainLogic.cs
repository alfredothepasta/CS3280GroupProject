using GroupProject.Data;
using GroupProject.DataObjects;
using GroupProject.Enum;
using GroupProject.Items;
using GroupProject.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    internal class clsMainLogic
    {
        /// <summary>
        /// The object for accessing the database
        /// </summary>
        private clsDataAccess _database;

        /// <summary>
        /// Holds the sql for items
        /// </summary>
        private clsItemsSQL _itemsSQL;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="Exception"></exception>
        public clsMainLogic()
        {
            try
            {
                _database = new clsDataAccess();
                _itemsSQL = new clsItemsSQL();
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
        /// Gets the list of items
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Item> getItemList()
        {
            try
            {
                List<Item> list = new List<Item>();

                int returnedItems = 0;
                string sSql = _itemsSQL.GetAllItems();
                DataSet itemsList = _database.ExecuteSQLStatement(sSql, ref returnedItems);

                foreach (DataTable dt in itemsList.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Item item = new Item()
                        {
                            ItemCode = (string)dr[0],
                            ItemDesc = (string)dr[1],
                            Cost = (decimal)dr[2],
                        };
                        list.Add(item);
                    }
                }

                return list;
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
        /// Creates a new invoice from the given inputs
        /// </summary>
        /// <param name="_invoiceDate"></param>
        /// <param name="_totalCost"></param>
        /// <param name="displayItems"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int createNewInvoice(DateTime? _invoiceDate, decimal _totalCost, List<DataDisplayItem> displayItems)
        {
            try
            {
                // return value
                int invoiceNum = 0;

                Invoice invoice = new Invoice()
                {
                    InvoiceDate = (DateTime)_invoiceDate,
                    TotalCost = _totalCost
                };

                string sSql = ClsMainSQL.AddInvoice(invoice);
                _database.ExecuteNonQuery(sSql);

                sSql = ClsMainSQL.GetLatestInvoice();
                var ds = _database.ExecuteScalarSQL(sSql);

                

                try
                {
                    invoiceNum = Int32.Parse(ds);
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }

                
                addLineItems(displayItems, invoiceNum);


                return invoiceNum;
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
        /// Adds line items 
        /// </summary>
        /// <param name="displayItems"></param>
        /// <param name="invoiceNum"></param>
        /// <exception cref="Exception"></exception>
        private void addLineItems(List<DataDisplayItem> displayItems, int invoiceNum)
        {
            try
            {
                List<LineItem> lineItems = new List<LineItem>();
                for (int iDataIndex = 0; iDataIndex < displayItems.Count; iDataIndex++)
                {
                    LineItem newLineItem = new LineItem()
                    {
                        InvoiceNum = invoiceNum,
                        LineItemNum = iDataIndex + 1,
                        ItemCode = displayItems[iDataIndex].ItemCode,
                        Quantity = displayItems[iDataIndex].Quantity
                    };
                    lineItems.Add(newLineItem);
                }

                foreach (LineItem lineItem in lineItems)
                {
                    string sql = ClsMainSQL.AddLineItem(lineItem);
                    _database.ExecuteNonQuery(sql);
                }


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
        /// gets the itmes listed in an invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<DataDisplayItem> getInvoiceItemList(int invoiceId)
        {
            try
            {
                int retVal = 0;
                string sSql = ClsMainSQL.GetDisplayItemsByInvoiceId(invoiceId);
                DataSet lineItems = _database.ExecuteSQLStatement(sSql, ref retVal);
                DataTable table = lineItems.Tables[0];
                List<DataDisplayItem> returnList = new List<DataDisplayItem>();
                foreach (DataRow row in table.Rows)
                {
                    DataDisplayItem item = new DataDisplayItem()
                    {
                        ItemCode = (string)row.ItemArray[0],
                        ItemName = (string)row.ItemArray[1],
                        ItemCost = (decimal)row.ItemArray[2],
                        Quantity = (int)row.ItemArray[3],

                    };
                    item.TotalCost = (decimal)item.ItemCost * (decimal)item.Quantity;

                    returnList.Add(item);

                }

                return returnList;
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
        /// Gets the date of the invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DateTime GetInvoiceDate(int invoiceId)
        {
            try
            {
                int retVal = 0;
                string sSql = ClsMainSQL.GetInvoiceById(invoiceId);
                DataSet invoiceDb = _database.ExecuteSQLStatement(sSql, ref retVal);
                DateTime returnDate = (DateTime)invoiceDb.Tables[0].Rows[0].ItemArray[1];

                return returnDate;
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
        /// Updates the invoice with the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="totalCost"></param>
        /// <param name="displayItems"></param>
        /// <exception cref="Exception"></exception>
        internal void UpdateInvoice(int invoiceId, decimal totalCost, List<DataDisplayItem> displayItems)
        {
            try
            {
                string sSql = ClsMainSQL.DeleteLineItems(invoiceId);
                _database.ExecuteNonQuery(sSql);

                sSql = ClsMainSQL.UpdateInvoices(totalCost, invoiceId);
                _database.ExecuteNonQuery(sSql);

                addLineItems(displayItems, invoiceId);
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
        /// Updates the data in the view model to match the data in the database
        /// </summary>
        /// <param name="mainViewModel"></param>
        /// <exception cref="Exception"></exception>
        internal void updateCurrentInvoiceDisplay(MainViewModel mainViewModel)
        {
            try
            {
                foreach(DataDisplayItem lineItem in mainViewModel.Data)
                {
                    Item item = mainViewModel.Items.Where(x => x.ItemCode == lineItem.ItemCode).First();

                    int indexLineItem = mainViewModel.Data.IndexOf(lineItem);

                    mainViewModel.Data[indexLineItem].ItemCost = item.Cost;
                    mainViewModel.Data[indexLineItem].ItemName = item.ItemDesc;
                    mainViewModel.Data[indexLineItem].TotalCost = lineItem.ItemCost * (decimal) lineItem.Quantity;
                }
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