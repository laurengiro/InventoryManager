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
                    Name = addSupplierViewModel.Name,
                    Country = addSupplierViewModel.Country
                };

                context.Suppliers.Add(newSupplier);
                context.SaveChanges();

                return Redirect("/Supplier");
            }

            return View(addSupplierViewModel);
        }

        public IActionResult Edit(int id)
        {
            Supplier theSupplier = context.Suppliers.Single(s => s.ID == id);
            EditSupplierViewModel editSupplierViewModel = new EditSupplierViewModel()
            {
                ID = theSupplier.ID,
                Name = theSupplier.Name,
                Country = theSupplier.Country
            };

            return View(editSupplierViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditSupplierViewModel editSupplierViewModel)
        {
            if (ModelState.IsValid)
            {
                Supplier updateSupplier = context.Suppliers.Single(s => s.ID == editSupplierViewModel.ID);
                updateSupplier.Name = editSupplierViewModel.Name;
                updateSupplier.Country = editSupplierViewModel.Country;

                context.Update(updateSupplier);
                context.SaveChanges();

                return Redirect("/Supplier");
            }

            return View(editSupplierViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Suppliers";
            IList<Supplier> suppliers = context.Suppliers.ToList();
            return View(suppliers);
        }

        [HttpPost]
        public IActionResult Remove(int[] supplierIds)
        {
            foreach (int supplierId in supplierIds)
            {
                Supplier theSupplier = context.Suppliers.Single(s => s.ID == supplierId);
                context.Suppliers.Remove(theSupplier);
            }

            context.SaveChanges();

            return Redirect("/Supplier");
        }
    }
}
