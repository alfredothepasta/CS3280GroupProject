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



        private clsDataAccess _database;

        private clsItemsSQL _itemsSQL;

        private List<LineItem> _lineItems;
        // if we're getting a search invoice we'll grab that from the application controller
        // Create a list of the invoice items (I will be making data classes for this)
        // have methods to add to a new invoice
        // have a method to enable editing of an invoice
        // a bunch of other stuff that I won't think of until I'm actually implenting things
        // honestly I'm really bad at engineering a solution before I write the code

        public clsMainLogic()
        {
            _database = new clsDataAccess();
            _itemsSQL = new clsItemsSQL();
        }

        public List<Item> getItemList()
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

        public List<DataDisplayItem> getInvoiceList(int invoiceId)
        {
            int retVal = 0;
            string sSql = ClsMainSQL.GetDisplayItemsByInvoiceId(invoiceId);
            DataSet lineItems = _database.ExecuteSQLStatement(sSql, ref retVal);
            DataTable table = lineItems.Tables[0];
            List<DataDisplayItem> returnList = new List<DataDisplayItem>();
            foreach(DataRow row in table.Rows)
            {
                DataDisplayItem item = new DataDisplayItem()
                {
                    ItemCode = (string)row.ItemArray[0],
                    ItemName = (string)row.ItemArray[1],
                    ItemCost = (decimal)row.ItemArray[2],
                    Quantity = (int)row.ItemArray[3],

                };
                item.TotalCost = (decimal) item.ItemCost * (decimal) item.Quantity;

                returnList.Add(item);

            }

            return returnList;
        }

        public DateTime GetInvoiceDate(int currentInvoiceId)
        {
            int retVal = 0;
            string sSql = ClsMainSQL.GetInvoiceById(currentInvoiceId);
            DataSet invoiceDb = _database.ExecuteSQLStatement(sSql, ref retVal);
            DateTime returnDate = (DateTime) invoiceDb.Tables[0].Rows[0].ItemArray[1];

            return returnDate;
        }

        internal void UpdateInvoice(int currentInvoiceId, decimal totalCost, List<DataDisplayItem> displayItems)
        {
            string sSql = ClsMainSQL.DeleteLineItems(currentInvoiceId);
            _database.ExecuteNonQuery(sSql);

            sSql = ClsMainSQL.UpdateInvoices(totalCost, currentInvoiceId);
            _database.ExecuteNonQuery(sSql);

            addLineItems(displayItems, currentInvoiceId);
        }
    }
}