using InventoryManager.Models;
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
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options)
            : base(options)
        { }
    }
}
