using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.ViewModels
{
    public class AddSaleViewModel
    {
        [Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Units Sold")]
        public int UnitsSold { get; set; }

        [Required]
        [Display(Name = "Item")]
        public int ItemID { get; set; }

        public List<SelectListItem> Items { get; set; }

        public AddSaleViewModel() { }

        public AddSaleViewModel(IEnumerable<Item> items)
        {
            Items = new List<SelectListItem>();
            foreach (Item item in items)
            {
                Items.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.SKU
                });
            }
        }
    }
}
