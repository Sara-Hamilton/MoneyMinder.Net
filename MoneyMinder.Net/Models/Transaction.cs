using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMinder.Net.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 100000, ErrorMessage = "Price must be between $0.01 and $100,000")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal PreviousTotal { get; set; } = 0;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int FundId { get; set; }
        public virtual Fund Fund { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Transaction() { }

        public Transaction(string description, string type, DateTime date, decimal amount, int categoryId, int fundId, int id = 0)
        {
            Description = description;
            Type = type;
            Date = date;
            Amount = amount;
            CategoryId = categoryId;
            FundId = fundId;
            TransactionId = id;
        }

        public override bool Equals(System.Object otherTransaction)
        {
            if (!(otherTransaction is Transaction))
            {
                return false;
            }
            else
            {
                Transaction newTransaction = (Transaction)otherTransaction;
                return this.TransactionId.Equals(newTransaction.TransactionId);
            }
        }

        public override int GetHashCode()
        {
            return this.TransactionId.GetHashCode();
        }

    }
}
