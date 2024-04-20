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
    /// <summary>
    /// A limited functionality search window
    /// </summary>
    internal class ClsFakeSearchLogic
    {
        /// <summary>
        /// database access object
        /// </summary>
        private clsDataAccess _database;
        
        /// <summary>
        /// constructor
        /// </summary>
        public ClsFakeSearchLogic()
        {
            _database = new clsDataAccess();
        }

        /// <summary>
        /// Gets the list of invoice numbers
        /// </summary>
        /// <returns></returns>
        public List<int> GetInvoiceNumbers()
        {
            try
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
