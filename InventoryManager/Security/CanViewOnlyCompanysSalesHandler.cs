using InventoryManager.Data;
using InventoryManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryManager.Security
{
    public class CanViewOnlyCompanysSalesHandler : AuthorizationHandler<ManageSaleAccessRequirement, Sale>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageSaleAccessRequirement requirement,
            Sale resource)
        {

            if (resource.Item.Company.ToLower() == context.User.Claims.FirstOrDefault(c => c.Type == "Company").Value.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}