using Microsoft.EntityFrameworkCore;
using MoneyMinder.Net.Models;
using MoneyMinder.Net.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoneyMinder.Net.Tests.Models
{
    public class TestDbContext : MoneyDbContext
    {
        public override DbSet<Category> Categories { get; set; }
        public override DbSet<Fund> Funds { get; set; }
        public override DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost; Port=8889; database=money_minder_net_test;uid=root;pwd=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
