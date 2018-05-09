using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMinder.Net.Models
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> Transactions { get; }
        Transaction Save(Transaction transaction);
        Transaction Edit(Transaction transaction);
        void Remove(Transaction transaction);
        void DeleteAll();
    }
}
