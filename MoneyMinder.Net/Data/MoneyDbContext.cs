using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Net.Models;

namespace MoneyMinder.Net.Data
{
    public class MoneyDbContext2 : IdentityDbContext<ApplicationUser>
    {
        public MoneyDbContext2()
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Fund> Funds { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=8889;database=money_minder_net;uid=root;pwd=root;");
        }

        public MoneyDbContext2(DbContextOptions<MoneyDbContext2> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Fund>().ToTable("Funds");
            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<ApplicationUser>(entity => {
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
            });
            builder.Entity<IdentityRole>(entity => {
                entity.Property(m => m.Name).HasMaxLength(127); entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
            builder.Entity<ApplicationUser>()
            .HasIndex(m => new { m.Email })
            .IsUnique(true);
        }

    }
}
