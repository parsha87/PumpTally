using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

  public interface IProductService
  {
    Task<int> AddEditProduct(ProductViewModel model);
    Task<List<ProductViewModel>> GetProduct();
    Task<ProductViewModel> GetProductById(int userId);
    Task<bool> DeleteProductById(int id);

  }
  public class ProductService : IProductService
  {
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;
    private PumpDBContext _pumpDBContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    public ProductService(ILogger<ProductService> logger,
        IWebHostEnvironment webHostEnvironment,
            PumpDBContext pumpDBContext,
            IMapper mapper,
            IConfiguration config
        )
    {
      _mapper = mapper;
      _logger = logger;
      _pumpDBContext = pumpDBContext;
      _webHostEnvironment = webHostEnvironment;
      _config = config;
    }
    public async Task<int> AddEditProduct(ProductViewModel model)
    {
      try
      {
        if (model.Id == 0)
        {

          Product product = new Product()
          {

            ProductName = model.ProductName,
            PackUnit = model.PackUnit,
            CodeNo = model.CodeNo,
            Qty = model.Qty,
            PurchasePrice = model.PurchasePrice,
            SalesPrice = model.SalesPrice,

          };
          await _pumpDBContext.AddAsync(product);
          await _pumpDBContext.SaveChangesAsync();
          ProductViewModel umodel = _mapper.Map<ProductViewModel>(product);
          return product.Id;
        }
        else
        {
          Product product = await _pumpDBContext.Products.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          product.ProductName = model.ProductName;
          product.PackUnit = model.PackUnit;
          product.CodeNo = model.CodeNo;
          product.Qty = model.Qty;
          product.PurchasePrice = model.PurchasePrice;
          product.SalesPrice = model.SalesPrice;
          await _pumpDBContext.SaveChangesAsync();
          return product.Id;
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }

    public async Task<List<ProductViewModel>> GetProduct()
    {
      try
      {
        List<Product> product = await _pumpDBContext.Products.ToListAsync();
        List<ProductViewModel> productViewModel = _mapper.Map<List<ProductViewModel>>(product);
        return productViewModel;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<ProductViewModel> GetProductById(int productId)
    {
      try
      {
        Product product = await _pumpDBContext.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
        ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
        return productViewModel;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<bool> DeleteProductById(int id)
    {
      try
      {
        Product model = await _pumpDBContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
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
