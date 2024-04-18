using GroupProject.Controller;
using GroupProject.Data;
using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.FakeSearch
{
    internal class ClsFakeSearchLogic
    {
        private clsDataAccess _database;
        

        public ClsFakeSearchLogic()
        {
            _database = new clsDataAccess();
        }

        public List<int> GetInvoiceNumbers()
        {
            // yes this should be in a separate class, I know
            string sSql = "SELECT InvoiceNum From Invoices;";
            int iRetVal = 0;
            DataSet invoiceNums = _database.ExecuteSQLStatement(sSql, ref iRetVal);
            List<int> result = new List<int>();

            for(int i = 0; i < iRetVal; i++)
            {
                result.Add((int)invoiceNums.Tables[0].Rows[i].ItemArray[0]);
            }

            return result;
        }
    }
}
