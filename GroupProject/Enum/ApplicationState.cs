using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Enum
{
    /// <summary>
    /// holds the states of the application
    /// </summary>
    public enum ApplicationState
    {
        Default,
        CreatingNewInvoice,
        ViewingInvoice,
        EditingInvoice,
        EditingRow,
        SearchingInvoice,
        EditingItemList
    }
}
