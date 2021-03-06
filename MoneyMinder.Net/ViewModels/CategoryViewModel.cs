﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMinder.Net.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Key]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
