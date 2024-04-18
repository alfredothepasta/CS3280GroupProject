using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.DataObjects
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{ItemDesc}";
        }

        
    }
}
