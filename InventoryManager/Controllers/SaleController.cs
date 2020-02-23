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
    public class SaleController : Controller
    {
        private InventoryManagerDbContext context;
        IAuthorizationService _authorizationService;

        public SaleController(InventoryManagerDbContext dbContext, IAuthorizationService authorizationService)
        {
            context = dbContext;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {          
            IList<Sale> sales = context.Sales.Include(i => i.Item).ToList();
            IList<Sale> usersSales = new List<Sale>();
            foreach (Sale sale in sales)
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, sale, new ManageSaleAccessRequirement());
                if (authorizationResult.Succeeded)
                {
                    usersSales.Add(sale);
                }
            }
            return View(usersSales);
        }

        public async Task<IActionResult> Add()
        {
            IList<Item> items = context.Items.Include(s => s.Supplier).ToList();
            IList<Item> usersItems = new List<Item>();
            foreach (Item item in items)
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, item, new ManageItemAccessRequirement());
                if (authorizationResult.Succeeded)
                {
                    usersItems.Add(item);
                }
            }
            AddSaleViewModel addSaleViewModel = new AddSaleViewModel(usersItems);
            return View(addSaleViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddSaleViewModel addSaleViewModel)
        {
            if (ModelState.IsValid)
            {
                Item newSaleItem =
                    context.Items.Single(i => i.ID == addSaleViewModel.ItemID);
                Sale newSale = new Sale
                {
                    Date = addSaleViewModel.Date,
                    UnitsSold = addSaleViewModel.UnitsSold,
                    Item = newSaleItem
                };

                context.Sales.Add(newSale);
                context.SaveChanges();

                return Redirect("/Sale");
            }

            return View(addSaleViewModel);
        }
    }
}
