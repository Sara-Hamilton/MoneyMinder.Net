using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Net.Models;

namespace MoneyMinder.Net.Data
{
    public class MoneyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MoneyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity => {
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
            });
            builder.Entity<IdentityRole>(entity => {
                entity.Property(m => m.Name).HasMaxLength(127); entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
