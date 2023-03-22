using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWepAPI.Models
{
    // 
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser>   ApplicationUsers { get; set; }
        protected ApplicationContext()
        {

        }
    }
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
