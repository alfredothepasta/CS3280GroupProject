using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    /// <summary>
    /// for data that gets displayed in the data grid
    /// </summary>
    public class DataDisplayItem()
    {
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Item Cost")]
        public decimal ItemCost { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as DataDisplayItem;
            if (item == null) return false;
            return this.ItemCode == item.ItemCode;
        }
    }
}
