﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMinder.Net.Models
{
    [Table("Funds")]
    public class Fund
    {
        [Key]
        public int FundId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Minimum { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Goal { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Fund() { }

        public Fund(string name, int id =0)
        {
            Name = name;
            FundId = id;
        }

        public override bool Equals(System.Object otherFund)
        {
            if (!(otherFund is Fund))
            {
                return false;
            }
            else
            {
                Fund newFund = (Fund)otherFund;
                return this.FundId.Equals(newFund.FundId);
            }
        }

        public override int GetHashCode()
        {
            return this.FundId.GetHashCode();
        }
    }
}