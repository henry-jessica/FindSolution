using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ProductModel
{
    public class GRN
    {
        [Key]
        public int GrnID { get; set; }

        public string OrderDate { get; set; }

        public string? DeliveryDate { get; set; }

        public Boolean StockUpdated { get; set; }
    }
}


