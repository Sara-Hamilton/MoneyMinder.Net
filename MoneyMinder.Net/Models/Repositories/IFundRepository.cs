using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMinder.Net.Models
{
    public interface IFundRepository
    {
        IQueryable<Fund> Funds { get; }
        Fund Save(Fund fund);
        Fund Edit(Fund fund);
        void Remove(Fund fund);
        void DeleteAll();
    }
}
