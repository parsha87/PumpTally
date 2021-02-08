using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class Msmeterdip
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? MsMeter1 { get; set; }
        public decimal? MsMeter2 { get; set; }
        public decimal? MsDip1 { get; set; }
        public decimal? MsTotal1 { get; set; }
        public decimal? MsMeter3 { get; set; }
        public decimal? MsMeter4 { get; set; }
        public decimal? MsDip2 { get; set; }
        public decimal? MsTotal2 { get; set; }
        public int? Shift { get; set; }
    }
}
