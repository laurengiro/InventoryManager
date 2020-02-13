using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.ViewModels
{
    public class EditItemViewModel : AddItemViewModel
    {
        public int ID { get; set; }

        public EditItemViewModel() { }

        public EditItemViewModel(IEnumerable<Supplier> suppliers)
        {
            Suppliers = new List<SelectListItem>();
            foreach (Supplier supplier in suppliers)
            {
                Suppliers.Add(new SelectListItem
                {
                    Value = supplier.ID.ToString(),
                    Text = supplier.Name
                });
            }
        }
    }
}