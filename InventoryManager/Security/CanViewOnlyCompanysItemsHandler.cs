﻿using InventoryManager.Data;
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
    public class CanViewOnlyCompanysItemsHandler : AuthorizationHandler<ManageItemAccessRequirement, Item>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageItemAccessRequirement requirement, Item resource)
        {
            var authFilterContext = context.Resource as AuthorizationFilterContext;
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            //if (resource.Company.ToLower() == context.User.FindFirst(ClaimTypes.Company).Value)
            if (resource.Company.ToLower() == context.User.FindFirst("Company").Value.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}