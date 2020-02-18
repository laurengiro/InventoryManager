using InventoryManager.Data;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            IList<Item> items = context.Items.Include(s => s.Supplier).ToList();
            return View(items);
        }

        public IActionResult Add()
        {
            AddItemViewModel addItemViewModel = new AddItemViewModel(context.Suppliers.ToList());
            return View(addItemViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddItemViewModel addItemViewModel)
        {
            if (ModelState.IsValid)
            {
                Supplier newItemSupplier =
                    context.Suppliers.Single(s => s.ID == addItemViewModel.SupplierID);
                // Add the new item to my existing items
                Item newItem = new Item
                {
                    SKU = addItemViewModel.SKU,
                    Description = addItemViewModel.Description,
                    QuantityOnHand = addItemViewModel.QuantityOnHand,
                    UnitCost = addItemViewModel.UnitCost,
                    SKUTotalValue = addItemViewModel.QuantityOnHand * addItemViewModel.UnitCost,
                    Company = User.Claims.FirstOrDefault(c => c.Type == "Company").Value,
                    Supplier = newItemSupplier
                };

                context.Items.Add(newItem);
                context.SaveChanges();

                return Redirect("/Item");
            }

            return View(addItemViewModel);
        }

        public IActionResult Edit(int id)
        {
            Item theItem = context.Items.Single(i => i.ID == id);
            EditItemViewModel editItemViewModel = new EditItemViewModel(context.Suppliers.ToList())
            {
                ID = theItem.ID,
                SKU = theItem.SKU,
                Description = theItem.Description,
                QuantityOnHand = theItem.QuantityOnHand,
                UnitCost = theItem.UnitCost,
                SupplierID = theItem.SupplierID
            };
            return View(editItemViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditItemViewModel editItemViewModel)
        {
            if (ModelState.IsValid)
            {
                Item updateItem = context.Items.Single(i => i.ID == editItemViewModel.ID);
                updateItem.SKU = editItemViewModel.SKU;
                updateItem.Description = editItemViewModel.Description;
                updateItem.QuantityOnHand = editItemViewModel.QuantityOnHand;
                updateItem.UnitCost = editItemViewModel.UnitCost;
                updateItem.SKUTotalValue = editItemViewModel.QuantityOnHand * editItemViewModel.UnitCost;
                updateItem.Supplier = context.Suppliers.Single(s => s.ID == editItemViewModel.SupplierID);

                context.Update(updateItem);
                context.SaveChanges();

                return Redirect("/Item");
            }

            return View(editItemViewModel);
        }

        //ADDED FOR CLAIMS
        //[Authorize(Policy = "AccessItemPolicy")]
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
                Item theItem = context.Items.Single(i => i.ID == itemId);
                context.Items.Remove(theItem);
            }

            context.SaveChanges();

            return Redirect("/Item");
        }
    }
}
