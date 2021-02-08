using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class Calibratation
    {
        public int? Dip { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Diff { get; set; }
    }
}
