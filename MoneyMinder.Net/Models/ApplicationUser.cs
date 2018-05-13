using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MoneyMinder.Net.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? UserTotal { get; set; } = 0;
        //public bool isAuthorized { get; set; }
        //public bool isActive { get; set; }
    }
}
