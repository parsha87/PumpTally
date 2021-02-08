using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.ViewModels
{
  public class ProductViewModel
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
