using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    [Serializable]
    public class ProductViewModel
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("reorderLevel")]
        public int ReorderLevel { get; set; }
        [JsonProperty("reorderQuantity")]
        public int ReorderQuantity { get; set; }
        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }
        [JsonProperty("stockOnHand")]
        public int StockOnHand { get; set; }
    }
}
