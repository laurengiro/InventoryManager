using InventoryManager.Data;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private InventoryManagerDbContext context;

        public ItemController(InventoryManagerDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Item> items = context.Items.Include(i => i.Supplier).ToList();
            return View(items);
        }

        public IActionResult Add()
        {
            AddItemViewModel addItemViewModel = new AddItemViewModel();
            return View(addItemViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddItemViewModel addItemViewModel)
        {
            if (ModelState.IsValid)
            {
                Supplier newItemSupplier =
                    context.Suppliers.Single(s => s.ID == addItemViewModel.SupplierID);
                // Add the new cheese to my existing cheeses
                Item newItem = new Item
                {
                    SKU = addItemViewModel.SKU,
                    Description = addItemViewModel.Description,
                    QuantityOnHand = addItemViewModel.QuantityOnHand,
                    UnitCost = addItemViewModel.UnitCost,
                    Supplier = newItemSupplier
                };

                context.Items.Add(newItem);
                context.SaveChanges();

                return Redirect("/Item");
            }

            return View(addItemViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Items";
            IList<Item> items = context.Items.ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult Remove(int[] itemIds)
        {
            foreach (int itemId in itemIds)
            {
                Item theItem = context.Items.Single(c => c.ID == itemId);
                context.Items.Remove(theItem);
            }

            context.SaveChanges();

            return Redirect("/");
        }
    }
}
