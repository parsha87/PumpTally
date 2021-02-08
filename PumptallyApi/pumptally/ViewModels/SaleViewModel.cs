using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.ViewModels
{
  public class SaleViewModel
  {
    public int Id { get; set; }
    public string DateofSale { get; set; }
    public int? ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal? Qty { get; set; }

    public decimal? QtyPurchased { get; set; }

    public decimal? Rate { get; set; }
    public decimal? Amount { get; set; }
    public int? Shift { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UserId { get; set; }
    public bool? Inedit { get; set; }
  }

  public class Datemodel
  {
    public string Datetime { get; set; }
  }
}
