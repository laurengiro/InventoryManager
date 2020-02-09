using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.ViewModels
{
    public class AddItemViewModel
    {
        [Required]
        [Display(Name = "Item # (SKU)")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "You must give your item a description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Quantity On Hand")]
        public int QuantityOnHand { get; set; }

        [Required]
        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        public List<SelectListItem> Suppliers { get; set; }


        public AddItemViewModel() { }

        public AddItemViewModel(IEnumerable<Supplier> suppliers)
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
