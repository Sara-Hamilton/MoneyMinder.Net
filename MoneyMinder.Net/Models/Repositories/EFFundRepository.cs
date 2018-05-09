using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyMinder.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace MoneyMinder.Net.Models
{
    public class EFFundRepository : IFundRepository
    {
        MoneyDbContext db;
        public EFFundRepository()
        {
            db = new MoneyDbContext();
        }

        public EFFundRepository(MoneyDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Fund> Funds
        { get { return db.Funds; } }


        public Fund Save(Fund fund)
        {
            db.Funds.Add(fund);
            db.SaveChanges();
            return fund;
        }

        public Fund Edit(Fund fund)
        {
            db.Entry(fund).State = EntityState.Modified;
            db.SaveChanges();
            return fund;
        }

        public void Remove(Fund fund)
        {
            db.Funds.Remove(fund);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.Funds.RemoveRange(db.Funds);
            db.SaveChanges();
        }
    }
}
