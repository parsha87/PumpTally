using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public DateTime? DateofSale { get; set; }
        public int? ProductId { get; set; }
        public decimal? Qty { get; set; }
        public decimal? QtyPurchased { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int? Shift { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
