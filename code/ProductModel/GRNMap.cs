using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;


namespace ProductModel
{
    public class GRNMap:ClassMap<GRN>
    {
        public GRNMap()
        {
            Map(m => m.GrnID).Name("GrnID");
            Map(m => m.OrderDate).Name("OrderDate");
            Map(m => m.DeliveryDate).Name("DeliveryDate");
            Map(m => m.StockUpdated).Name("StockUpdated");
        }

    }
}


