using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PackUnit { get; set; }
        public int? CodeNo { get; set; }
        public decimal? Qty { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalesPrice { get; set; }
    }
}
