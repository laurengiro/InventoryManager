using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.ViewModels
{
    public class AddSupplierViewModel
    {
        [Required]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}