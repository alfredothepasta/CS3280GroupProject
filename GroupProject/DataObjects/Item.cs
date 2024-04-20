using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.DataObjects
{
    /// <summary>
    /// Data class that holds items
    /// </summary>
    public class Item
    {
        /// <summary>
        /// holds the item code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// holds the item description
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// holds teh items cost
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// returns the string value for an item
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ItemDesc}";
        }

        
    }
}
