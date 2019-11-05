using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreM.Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Provider : Address
            builder.HasOne(f => f.Address)
                .WithOne(e => e.Provider);

            // 1 : N => Provider : Product
            builder.HasMany(f => f.Products)
                .WithOne(e => e.Provider)
                .HasForeignKey(e => e.ProviderId);

            builder.ToTable("Providers");
        }
    }
}
