﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }
        public decimal UnitCost { get; set; }
        public decimal SKUValue { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
    }
}
