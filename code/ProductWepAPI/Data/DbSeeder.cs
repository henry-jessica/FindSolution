using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ProductModel;

namespace ProductSeeding
{
  public class DbSeeder
  {
    private readonly ProductDBContext _ctx;
    private readonly IWebHostEnvironment _hosting;

    public DbSeeder(ProductDBContext ctx, IWebHostEnvironment hosting)
    {
      _ctx = ctx;
      _hosting = hosting;
    }

    public void Seed()
    {
      // _ctx.Database.EnsureDeleted();
      _ctx.Database.EnsureCreated();

      if(!_ctx.Suppliers.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "ProductModel.Supplier.csv");
                var json = File.ReadAllText(filepath);
                var suppliers = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(json);
                _ctx.Suppliers.AddRange(suppliers);
            }



            //if (!_ctx.Products.Any())
            //{
            //    int MaxId = _ctx.Products.Max(p => p.ID); // Alternative to using Autonumbered keys
            //    var filepath = Path.Combine(_hosting.ContentRootPath, "ProductModel.Products.csv");
            //    Product[] products = DBHelper.Get<Product>(filepath).ToArray();
            //    _ctx.Products.AddRange(products);


            //    _ctx.SaveChanges();

                //}
                //if (!_ctx.GRNLines.Any())
                //  {
                //      //int MaxId = _ctx.Products.Max(p => p.ID); // Alternative to using Autonumbered keys
                //      var filepath = Path.Combine(_hosting.ContentRootPath, "ProductModel.GRNLine.csv");
                //      GRNLine[] grnLines = DBHelper.Get<GRNLine>(filepath).ToArray();
                //       _ctx.GRNLines.AddRange(grnLines);

                //      _ctx.SaveChanges();

                //  }
                //  if (!_ctx.GRNs.Any())
                //  {
                //      //int MaxId = _ctx.Products.Max(p => p.ID); // Alternative to using Autonumbered keys
                //      var filepath = Path.Combine(_hosting.ContentRootPath, "ProductModel.GRN.csv");
                //      GRN[] grns = DBHelper.Get<GRN>(filepath).ToArray();
                //      _ctx.GRNs.AddRange(grns);
                //      _ctx.SaveChanges();


                //  }
            }

  }
}
