using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace ProductModel
{
    public class GRNLineMap : ClassMap<GRNLine>
    {
        public GRNLineMap()
        {
            Map(m => m.LineID).Name("LineID");
            Map(m => m.QtyDelivered).Name("QtyDelivered");
            Map(m => m.StockID).Name("StockID");
            Map(m => m.GrnId).Name("GrnId");
        }
    }
}
    

