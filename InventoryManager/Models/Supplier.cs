using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public IList<Item> Items { get; set; }
    }
}