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
                catch
                {
                    throw new Exception("Something went wrong.");
                }

                List<LineItem> items = new List<LineItem>();
                for (int iDataIndex = 0; iDataIndex < displayItems.Count; iDataIndex++)
                {
                    LineItem newLineItem = new LineItem()
                    {
                        InvoiceNum = invoiceNum,
                        LineItemNum = iDataIndex + 1,
                        ItemCode = displayItems[iDataIndex].ItemCode,
                        Quantity = displayItems[iDataIndex].Quantity
                    };
                    items.Add(newLineItem);
                }
                addLineItems(items);


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

        private void addLineItems(List<LineItem> lineItems)
        {
            try
            {
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
    }
}