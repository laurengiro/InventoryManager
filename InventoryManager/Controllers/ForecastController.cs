using InventoryManager.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{
    [Authorize]
    public class ForecastController
    {
        private InventoryManagerDbContext context;
        IAuthorizationService _authorizationService;

        public ForecastController(InventoryManagerDbContext dbContext, IAuthorizationService authorizationService)
        {
            context = dbContext;
            _authorizationService = authorizationService;
        }


    }
}
