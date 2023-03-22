
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductModel
{
    public class ProductDBContext : DbContext
    {
         public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<GRN> GRNs { get; set; }
        public DbSet<GRNLine> GRNLines { get; set; }

        public ProductDBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var myconnectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Week8ProductCoreDB";
            optionsBuilder.UseSqlServer(myconnectionstring)
              .LogTo(Console.WriteLine,
                     new[] { DbLoggerCategory.Database.Command.Name },
                     LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<GRN> csv_gRNS = DBHelper.Get<GRN, GRNMap>("ProductModel.GRN.csv");
            modelBuilder.Entity<GRN>().HasData(csv_gRNS);
            List<GRNLine> csv_gRNLineS = DBHelper.Get<GRNLine, GRNLineMap>("ProductModel.GRNLine.csv");
            modelBuilder.Entity<GRNLine>().HasData(csv_gRNLineS);
            List<Product> csv_products = DBHelper.Get<Product, Map>("ProductModel.Products.csv");
            modelBuilder.Entity<Product>().HasData(csv_products);

            List<Supplier> csv_suppliers = DBHelper.Get<Supplier, MapSupplier>("ProductModel.Supplier.csv");
            modelBuilder.Entity<Supplier>().HasData(csv_suppliers);

            base.OnModelCreating(modelBuilder);

            // Alternative to [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            //modelBuilder.Entity<Product>().Property("ID").UseIdentityColumn(seed: 1, increment: 1);

            //Product[] products = DBHelper.Get<Product>(@"C:\Users\ppowell\source\repos\EFCoreProductsSample\ProductModel\Products.csv").ToArray();
            //modelBuilder.Entity<Product>().HasData(products);

            //modelBuilder.Entity<Product>().HasData(
            // new Product
            // {
            //     ID = 46,
            //     Description = "test",
            //     ReorderLevel = 4,
            //     ReorderQuantity = 2,
            //     StockOnHand = 30,
            //     UnitPrice = 10
            // });


        }



    }
}


