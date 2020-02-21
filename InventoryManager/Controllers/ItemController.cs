using InventoryManager.Data;
using InventoryManager.Models;
using InventoryManager.Security;
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
        IAuthorizationService _authorizationService;

        public ItemController(InventoryManagerDbContext dbContext, IAuthorizationService authorizationService)
        {
            context = dbContext;
            _authorizationService = authorizationService;
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

        public async Task<IActionResult> Edit(int id)
        {
            Item theItem = context.Items.Single(i => i.ID == id);

            if (theItem ==  null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, theItem, new ManageItemAccessRequirement());
            if (authorizationResult.Succeeded)
            {
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
            else
            {
                return new ForbidResult();
            }
            
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

        public IActionResult DeleteMultiple()
        {
            ViewBag.title = "Delete Items";
            IList<Item> items = context.Items.ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult DeleteMultiple(int[] itemIds)
        {
            foreach (int itemId in itemIds)
            {
                Item theItem = context.Items.Single(i => i.ID == itemId);
                context.Items.Remove(theItem);
            }

            context.SaveChanges();

            return Redirect("/Item");
        }


        public async Task<IActionResult> AddInventory(int id)
        {
            Item theItem = context.Items.Single(i => i.ID == id);

            if (theItem == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, theItem, new ManageItemAccessRequirement());
            if (authorizationResult.Succeeded)
            {
                AddInventoryViewModel addInventoryViewModel = new AddInventoryViewModel()
                {
                    ID = theItem.ID,
                    SKU = theItem.SKU,
                    Description = theItem.Description,
                    QuantityOnHand = theItem.QuantityOnHand,
                    UnitCost = theItem.UnitCost,
                    SupplierID = theItem.SupplierID
                };
                return View(addInventoryViewModel);
            }
            else
            {
                return new ForbidResult();
            }

        }

        [HttpPost]
        public IActionResult AddInventory(AddInventoryViewModel addInventoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Item updateItem = context.Items.Single(i => i.ID == addInventoryViewModel.ID);
                int totalUnits = addInventoryViewModel.QuantityOnHand + addInventoryViewModel.ToAddQuantity;
                updateItem.QuantityOnHand = totalUnits;
                updateItem.UnitCost = ((decimal)addInventoryViewModel.QuantityOnHand / totalUnits) * addInventoryViewModel.UnitCost 
                    + ((decimal)addInventoryViewModel.ToAddQuantity / totalUnits) * addInventoryViewModel.ToAddUnitCost;
                updateItem.SKUTotalValue = addInventoryViewModel.QuantityOnHand * addInventoryViewModel.UnitCost + addInventoryViewModel.ToAddQuantity * addInventoryViewModel.ToAddUnitCost;

                context.Update(updateItem);
                context.SaveChanges();

                return Redirect("/Item");
            }

            return View(addInventoryViewModel);
        }
    }
}
