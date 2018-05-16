using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMinder.Net.ViewModels
{
    public class FundViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Key]
        [Display(Name = "Fund")]
        public int FundId { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}}", ApplyFormatInEditMode = true)]
        public decimal? Minimum { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? Goal { get; set; }
    }
}
