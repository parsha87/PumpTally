using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class Hsdmeterdip
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Hsdmeter1 { get; set; }
        public decimal? Hsdmeter2 { get; set; }
        public decimal? Hsddip1 { get; set; }
        public decimal? Hsdtotal1 { get; set; }
        public decimal? Hsdmeter3 { get; set; }
        public decimal? Hsdmeter4 { get; set; }
        public decimal? Hsddip2 { get; set; }
        public decimal? Hsdtotal2 { get; set; }
        public int? Shift { get; set; }
    }
}
