
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using pumptally.Data;
using pumptally.Data.Entities;
using pumptally.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.Services
{
  public interface ISalesService
  {
    Task<int> AddEditSales(SaleViewModel model);
    Task<List<Sale>> GetSales();
    Task<Sale> GetSalesById(int userId);
    Task<bool> DeleteSalesById(int id);
    Task<List<Sale>> GetSalesByDate(DateTime datetime);

    Task<int> AddEditVoucherBill(VoucherBillViewModel model);
    Task<List<VoucherBill>> GetVoucherBill();
    Task<VoucherBill> GetVoucherBillById(int Id);
    Task<bool> DeleteVoucherById(int id);
    Task<List<VoucherBill>> GetVoucherByDate(DateTime datetime);



    Task<int> AddEditPumpCash(PumpCashViewModel model);
    Task<List<PumpCash>> GetPumpCash();
    Task<PumpCash> GetPumpCashById(int Id);
    Task<bool> DeletePumpBillById(int id);
    Task<List<PumpCash>> GetPumpbillByDate(DateTime datetime);



    Task<int> AddEditBiscuitCash(BiscuitCashViewModel model);
    Task<List<BiscuitCash>> GetBiscuitCash();
    Task<BiscuitCash> GetBiscuitCashById(int Id);
    Task<bool> DeleteBiscuitBillById(int id);
    Task<List<BiscuitCash>> GetBiscuitbillByDate(DateTime datetime);



  }
  public class SalesService : ISalesService
  {
    private readonly ILogger<SalesService> _logger;
    private PumpDBContext _pumpDBContext;
    public SalesService(ILogger<SalesService> logger,
           PumpDBContext pumpDBContext
        )
    {
      _logger = logger;
      _pumpDBContext = pumpDBContext;
    }

    public async Task<int> AddEditSales(SaleViewModel model)
    {
      try
      {
        if (model.Id == 0)
        {
          Sale sale = new Sale()
          {
            DateofSale = Convert.ToDateTime(model.DateofSale),
            ProductId = model.ProductId,
            Qty = model.Qty,
            QtyPurchased = model.QtyPurchased,
            Rate = model.Rate,
            Amount = model.Amount,
            Shift = model.Shift,
            CreatedBy = model.UserId,
            CreatedOn = DateTime.Now,
            UpdatedBy = model.UserId,
            UpdatedOn = DateTime.Now
          };
          await _pumpDBContext.AddAsync(sale);
          await _pumpDBContext.SaveChangesAsync();
          //ProductViewModel umodel = _mapper.Map<SaleViewModel>(product);
          return sale.Id;
        }
        else
        {
          Sale sale = await _pumpDBContext.Sales.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          sale.DateofSale = Convert.ToDateTime(model.DateofSale);
          sale.ProductId = model.ProductId;
          sale.Qty = model.Qty;
          sale.QtyPurchased = model.QtyPurchased;
          sale.Rate = model.Rate;
          sale.Amount = model.Amount;
          sale.Shift = model.Shift;
          sale.UpdatedBy = model.UserId;
          sale.UpdatedOn = DateTime.Now;
          await _pumpDBContext.SaveChangesAsync();
          return sale.Id;
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }
    public async Task<List<Sale>> GetSales()
    {
      try
      {
        List<Sale> sale = await _pumpDBContext.Sales.ToListAsync();
        return sale;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<Sale> GetSalesById(int Id)
    {
      try
      {
        Sale sale = await _pumpDBContext.Sales.Where(x => x.Id == Id).FirstOrDefaultAsync();
        return sale;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<List<Sale>> GetSalesByDate(DateTime datetime)
    {
      try
      {

        List<Sale> sales = await _pumpDBContext.Sales.ToListAsync();
        sales = sales.Where(x => Convert.ToDateTime(x.DateofSale).Date == datetime.Date).ToList();
        return sales;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<bool> DeleteSalesById(int id)
    {
      try
      {
        Sale sale = await _pumpDBContext.Sales.Where(x => x.Id == id).FirstOrDefaultAsync();
        _pumpDBContext.Remove(sale);
        await _pumpDBContext.SaveChangesAsync();

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }



    public async Task<int> AddEditVoucherBill(VoucherBillViewModel model)
    {
      try
      {
        if (model.Id == 0)
        {
          VoucherBill modeltoSave = new VoucherBill()
          {
            Date = Convert.ToDateTime(model.Date),
            Description = model.Description,
            Ammount = model.Ammount,
            Shift = model.Shift,
            CreatedBy = model.UserId,
            CreatedOn = DateTime.Now,
            UpdatedBy = model.UserId,
            UpdatedOn = DateTime.Now
          };
          await _pumpDBContext.AddAsync(modeltoSave);
          await _pumpDBContext.SaveChangesAsync();
          //ProductViewModel umodel = _mapper.Map<SaleViewModel>(product);
          return modeltoSave.Id;
        }
        else
        {
          VoucherBill modeltoUpdate = await _pumpDBContext.VoucherBills.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          modeltoUpdate.Date = Convert.ToDateTime(model.Date);
          modeltoUpdate.Description = model.Description;
          modeltoUpdate.Ammount = model.Ammount;
          modeltoUpdate.Shift = model.Shift;
          modeltoUpdate.UpdatedBy = model.UserId;
          modeltoUpdate.UpdatedOn = DateTime.Now;
          await _pumpDBContext.SaveChangesAsync();
          return modeltoUpdate.Id;
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }
    public async Task<List<VoucherBill>> GetVoucherBill()
    {
      try
      {
        List<VoucherBill> sale = await _pumpDBContext.VoucherBills.ToListAsync();
        return sale;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<VoucherBill> GetVoucherBillById(int Id)
    {
      try
      {
        VoucherBill sale = await _pumpDBContext.VoucherBills.Where(x => x.Id == Id).FirstOrDefaultAsync();
        return sale;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<List<VoucherBill>> GetVoucherByDate(DateTime datetime)
    {
      try
      {

        List<VoucherBill> model = await _pumpDBContext.VoucherBills.ToListAsync();
        model = model.Where(x => Convert.ToDateTime(x.Date).Date == datetime.Date).ToList();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<bool> DeleteVoucherById(int id)
    {
      try
      {
        VoucherBill model = await _pumpDBContext.VoucherBills.Where(x => x.Id == id).FirstOrDefaultAsync();
        _pumpDBContext.Remove(model);
        await _pumpDBContext.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }




    public async Task<int> AddEditPumpCash(PumpCashViewModel model)
    {
      try
      {
        if (model.Id == 0)
        {
          PumpCash modeltoSave = new PumpCash()
          {
            Date = Convert.ToDateTime(model.Date),
            Description = model.Description,
            Amount = model.Amount,
            Shift = model.Shift,
            CreatedBy = model.UserId,
            CreatedOn = DateTime.Now,
            UpdatedBy = model.UserId,
            UpdatedOn = DateTime.Now
          };
          await _pumpDBContext.AddAsync(modeltoSave);
          await _pumpDBContext.SaveChangesAsync();
          //ProductViewModel umodel = _mapper.Map<SaleViewModel>(product);
          return modeltoSave.Id;
        }
        else
        {
          PumpCash modeltoUpdate = await _pumpDBContext.PumpCashes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          modeltoUpdate.Date = Convert.ToDateTime(model.Date);
          modeltoUpdate.Description = model.Description;
          modeltoUpdate.Amount = model.Amount;
          modeltoUpdate.Shift = model.Shift;
          modeltoUpdate.UpdatedBy = model.UserId;
          modeltoUpdate.UpdatedOn = DateTime.Now;
          await _pumpDBContext.SaveChangesAsync();
          return modeltoUpdate.Id;
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }
    public async Task<List<PumpCash>> GetPumpCash()
    {
      try
      {
        List<PumpCash> model = await _pumpDBContext.PumpCashes.ToListAsync();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<PumpCash> GetPumpCashById(int Id)
    {
      try
      {
        PumpCash model = await _pumpDBContext.PumpCashes.Where(x => x.Id == Id).FirstOrDefaultAsync();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<List<PumpCash>> GetPumpbillByDate(DateTime datetime)
    {
      try
      {

        List<PumpCash> model = await _pumpDBContext.PumpCashes.ToListAsync();
        model = model.Where(x => Convert.ToDateTime(x.Date).Date == datetime.Date).ToList();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<bool> DeletePumpBillById(int id)
    {
      try
      {
        PumpCash model = await _pumpDBContext.PumpCashes.Where(x => x.Id == id).FirstOrDefaultAsync();
        _pumpDBContext.Remove(model);
        await _pumpDBContext.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }



    public async Task<int> AddEditBiscuitCash(BiscuitCashViewModel model)
    {
      try
      {
        if (model.Id == 0)
        {
          BiscuitCash modeltoSave = new BiscuitCash()
          {
            Date = Convert.ToDateTime(model.Date),
            Description = model.Description,
            Amount = model.Amount,
            Shift = model.Shift,
            CreatedBy = model.UserId,
            CreatedOn = DateTime.Now,
            UpdatedBy = model.UserId,
            UpdatedOn = DateTime.Now
          };
          await _pumpDBContext.AddAsync(modeltoSave);
          await _pumpDBContext.SaveChangesAsync();
          //ProductViewModel umodel = _mapper.Map<SaleViewModel>(product);
          return modeltoSave.Id;
        }
        else
        {
          BiscuitCash modeltoUpdate = await _pumpDBContext.BiscuitCashes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          modeltoUpdate.Date = Convert.ToDateTime(model.Date);
          modeltoUpdate.Description = model.Description;
          modeltoUpdate.Amount = model.Amount;
          modeltoUpdate.Shift = model.Shift;
          modeltoUpdate.UpdatedBy = model.UserId;
          modeltoUpdate.UpdatedOn = DateTime.Now;
          await _pumpDBContext.SaveChangesAsync();
          return modeltoUpdate.Id;
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }

    public async Task<List<BiscuitCash>> GetBiscuitCash()
    {
      try
      {
        List<BiscuitCash> model = await _pumpDBContext.BiscuitCashes.ToListAsync();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<BiscuitCash> GetBiscuitCashById(int Id)
    {
      try
      {
        BiscuitCash model = await _pumpDBContext.BiscuitCashes.Where(x => x.Id == Id).FirstOrDefaultAsync();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<List<BiscuitCash>> GetBiscuitbillByDate(DateTime datetime)
    {
      try
      {

        List<BiscuitCash> model = await _pumpDBContext.BiscuitCashes.ToListAsync();
        model = model.Where(x => Convert.ToDateTime(x.Date).Date == datetime.Date).ToList();
        return model;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    public async Task<bool> DeleteBiscuitBillById(int id)
    {
      try
      {
        BiscuitCash model = await _pumpDBContext.BiscuitCashes.Where(x => x.Id == id).FirstOrDefaultAsync();
        _pumpDBContext.Remove(model);
        await _pumpDBContext.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }



  }

}
