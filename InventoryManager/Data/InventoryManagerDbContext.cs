using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Data
{
    public class InventoryManagerDbContext : IdentityDbContext
    {
        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options)
            : base(options)
        { }
    }
}
