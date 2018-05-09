using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyMinder.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace MoneyMinder.Net.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        MoneyDbContext db;
        public EFCategoryRepository()
        {
            db = new MoneyDbContext();
        }

        public EFCategoryRepository (MoneyDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Category> Categories
        { get { return db.Categories; } }


        public Category Save(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public Category Edit(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return category;
        }

        public void Remove(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.Categories.RemoveRange(db.Categories);
            db.SaveChanges();
        }
    }
}
