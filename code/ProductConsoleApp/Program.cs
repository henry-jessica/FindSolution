using ProductAPIClientLibrary;
using ProductModel;
using System;
using System.Globalization;
using System.Threading;
using ViewModels;

namespace ProductConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo Cirl = new CultureInfo("ie-IE");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //using (ProductDBContext db = new ProductDBContext())
            //{
            //    foreach (Product product in db.Products)
            //        Console.WriteLine("{0} Costs {1:C} ", product.Description,  product.UnitPrice);


            //}
            if (ProductClient.login("paul@itsligo.ie", "P@ssw0rd!"))
            {
                Console.WriteLine("Logged in");
                Console.WriteLine("Trying Authenticated Queries");

                ProductClient.PostProduct(new 
                    Product { ID=0, Description = "Nuts", 
                                UnitPrice = 2.0, StockOnHand = 200 });

                foreach (Product product in ProductClient.getProducts())
                {
                    Console.WriteLine("{0} Costs {1:C} ", product.Description, product.UnitPrice);
                }

            };
        }
    }
}
