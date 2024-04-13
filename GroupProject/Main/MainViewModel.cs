using GroupProject.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    /// <summary>
    /// Holds the stuff that we're binding to the data grid
    /// </summary>
    class MainViewModel
    {
        public List<DataDisplayItem> Data { get; set; }
        public List<Item> Items { get; set; }

        public decimal TotalCost { get; set; }
    }
}
