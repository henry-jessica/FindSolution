using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductModel
{
    public class GRNLine
    {
        [Key]
        public int LineID { get; set; }
        public int GrnId { get; set; }
        public int StockID { get; set; }
        public int QtyDelivered { get; set; }

    }
}


