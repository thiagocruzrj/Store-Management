using Microsoft.EntityFrameworkCore;
using StoreM.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreM.Data.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Default mapping */
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            /* Used to mapping all entities */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);

            /* Unable cascade delete */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
