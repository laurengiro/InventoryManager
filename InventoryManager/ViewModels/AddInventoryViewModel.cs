using InventoryManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.ViewModels
{
    public class AddInventoryViewModel : AddItemViewModel
    {
        public int ID { get; set; }

        public int ToAddQuantity { get; set; }

        public decimal ToAddUnitCost { get; set; }
    }
}
