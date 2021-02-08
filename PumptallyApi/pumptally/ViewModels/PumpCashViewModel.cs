using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.ViewModels
{
  public class PumpCashViewModel
  {
    public int Id { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
    public decimal? Amount { get; set; }
    public int? Shift { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int UserId { get; set; }
    public bool? Inedit { get; set; }

  }
}
