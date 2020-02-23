using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int UnitsSold { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }
    }
}