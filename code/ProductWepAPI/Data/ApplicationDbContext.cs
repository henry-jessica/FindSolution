using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWepAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // Seeding to be done in Seeder after we add the context
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var myconnectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Week7ProductCoreDB";
            optionsBuilder.UseSqlServer(myconnectionstring);

            base.OnConfiguring(optionsBuilder);
        }

        

    }
}
