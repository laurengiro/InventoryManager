using InventoryManager.Data;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{
    public class SupplierController : Controller
    {
        private readonly InventoryManagerDbContext context;

        public SupplierController(InventoryManagerDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Supplier> suppliers = context.Suppliers.ToList();

            return View(suppliers);
        }

        public IActionResult Add()
        {
            AddSupplierViewModel addSupplierViewModel = new AddSupplierViewModel();
            return View(addSupplierViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddSupplierViewModel addSupplierViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new supplier to the list of suppliers
                Supplier newSupplier = new Supplier
                {
                    Name = addSupplierViewModel.Name
                };

                context.Suppliers.Add(newSupplier);
                context.SaveChanges();

                return Redirect("/Supplier");
            }

            return View(addSupplierViewModel);
        }
    }
}
