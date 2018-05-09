using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyMinder.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace MoneyMinder.Net.Models
{
    public class EFTransactionRepository : ITransactionRepository
    {
        MoneyDbContext db;
        public EFTransactionRepository()
        {
            db = new MoneyDbContext();
        }

        public EFTransactionRepository(MoneyDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Transaction> Transactions
        { get { return db.Transactions; } }


        public Transaction Save(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
            return transaction;
        }

        public Transaction Edit(Transaction transaction)
        {
            db.Entry(transaction).State = EntityState.Modified;
            db.SaveChanges();
            return transaction;
        }

        public void Remove(Transaction transaction)
        {
            db.Transactions.Remove(transaction);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.Transactions.RemoveRange(db.Transactions);
            db.SaveChanges();
        }
    }
}
