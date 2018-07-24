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
        public bool ShowMinAndGoal { get; set; }
    }
}
