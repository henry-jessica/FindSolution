﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModel
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Number")]
        public int ID { get; set; }
        public string Description { get; set; }
        [Display(Name = "Re-order Threshold")]
        public int ReorderLevel { get; set; }
        [Display(Name = "Re-order Quantity")]
        public int ReorderQuantity { get; set; }
        [Display(Name = "Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Stock on Hand")]
        public int StockOnHand { get; set; }
    }
}