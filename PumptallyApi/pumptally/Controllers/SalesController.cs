using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using pumptally.Data;
using pumptally.Data.Entities;
using pumptally.Services;
using pumptally.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.Controllers
{
  [EnableCors("AllowSpecificOrigin")]
  [Route("api/[controller]")]
  [ApiController]
  public class SalesController : ControllerBase
  {
    private readonly ILogger<SalesController> _logger;
    private readonly IMapper _mapper;
    private readonly PumpDBContext _pumpDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    private ISalesService _salesService;
    private IProductService _productService;


    public SalesController(IProductService productService, ISalesService salesService, ILogger<SalesController> logger,
        IConfiguration config, PumpDBContext pumpDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
    {
      _productService = productService;
      _salesService = salesService;
      _logger = logger;
      _mapper = mapper;
      _config = config;
      _pumpDbContext = pumpDbContext;
      _webHostEnvironment = hostingEnvironment;
    }



    [HttpGet]
    public async Task<ActionResult<List<Sale>>> Get()
    {
      try
      {
        List<Sale> model = await _salesService.GetSales();
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> Get(int id)
    {
      try
      {
        Sale model = await _salesService.GetSalesById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("GetSalesByDate/{date}")]
    public async Task<ActionResult<List<Sale>>> GetSalesByDate(string date)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(date);
        List<Sale> model = await _salesService.GetSalesByDate(dt);
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SaleViewModel model)
    {
      try
      {
        //show error is project is already present with given project name
        //bool isProductExists = _pumpDbContext.Products.Any(x => x.ProductName.ToUpper() == model.ProductName.ToUpper());
        //if (isProductExists)
        //{
        //  ModelState.AddModelError("Error", "Project with given project number already exists.");
        //  return BadRequest(ModelState);
        //}
        int productId = await _salesService.AddEditSales(model);
        model.Id = productId;
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Post) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpGet("DeleteSale/{id}")]
    public async Task<ActionResult> DeleteSale(int id)
    {
      try
      {
        bool model = await _salesService.DeleteSalesById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }




    [HttpGet("GetVoucherBill")]
    public async Task<ActionResult<List<VoucherBill>>> GetVoucherBill()
    {
      try
      {
        List<VoucherBill> model = await _salesService.GetVoucherBill();
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("VoucherBillById/{id}")]
    public async Task<ActionResult<VoucherBill>> GetVoucherBillById(int id)
    {
      try
      {
        VoucherBill model = await _salesService.GetVoucherBillById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost("VoucherBill")]
    public async Task<IActionResult> PostVoucherBill([FromBody] VoucherBillViewModel model)
    {
      try
      {
        //show error is project is already present with given project name
        //bool isProductExists = _pumpDbContext.Products.Any(x => x.ProductName.ToUpper() == model.ProductName.ToUpper());
        //if (isProductExists)
        //{
        //  ModelState.AddModelError("Error", "Project with given project number already exists.");
        //  return BadRequest(ModelState);
        //}
        int id = await _salesService.AddEditVoucherBill(model);
        model.Id = id;
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Post) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }


    [HttpGet("GetVoucherByDate/{date}")]
    public async Task<ActionResult<List<VoucherBill>>> GetVoucherByDate(string date)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(date);
        List<VoucherBill> model = await _salesService.GetVoucherByDate(dt);
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("DeleteVoucher/{id}")]
    public async Task<ActionResult> DeleteVoucher(int id)
    {
      try
      {
        bool model = await _salesService.DeleteVoucherById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }



    [HttpGet("GetPumpCash")]
    public async Task<ActionResult<List<VoucherBill>>> GetPumpCash()
    {
      try
      {
        List<PumpCash> model = await _salesService.GetPumpCash();
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("PumpCashById/{id}")]
    public async Task<ActionResult<PumpCash>> GetPumpCashById(int id)
    {
      try
      {
        PumpCash model = await _salesService.GetPumpCashById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost("PumpCash")]
    public async Task<IActionResult> PostPumpCash([FromBody] PumpCashViewModel model)
    {
      try
      {
        //show error is project is already present with given project name
        //bool isProductExists = _pumpDbContext.Products.Any(x => x.ProductName.ToUpper() == model.ProductName.ToUpper());
        //if (isProductExists)
        //{
        //  ModelState.AddModelError("Error", "Project with given project number already exists.");
        //  return BadRequest(ModelState);
        //}
        int id = await _salesService.AddEditPumpCash(model);
        model.Id = id;
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Post) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
    [HttpGet("DeletePumpbill/{id}")]
    public async Task<ActionResult> DeletePumpbill(int id)
    {
      try
      {
        bool model = await _salesService.DeletePumpBillById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("GetPumpBillByDate/{date}")]
    public async Task<ActionResult<List<PumpCash>>> GetPumpBillByDate(string date)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(date);
        List<PumpCash> model = await _salesService.GetPumpbillByDate(dt);
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }




    [HttpGet("GetBisuitCash")]
    public async Task<ActionResult<List<BiscuitCash>>> GetBisuitCash()
    {
      try
      {
        List<BiscuitCash> model = await _salesService.GetBiscuitCash();
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("BiscuitCashById/{id}")]
    public async Task<ActionResult<BiscuitCash>> GetBiscuitCashById(int id)
    {
      try
      {
        BiscuitCash model = await _salesService.GetBiscuitCashById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost("BiscuitCash")]
    public async Task<IActionResult> PostBiscuitCash([FromBody] BiscuitCashViewModel model)
    {
      try
      {
        //show error is project is already present with given project name
        //bool isProductExists = _pumpDbContext.Products.Any(x => x.ProductName.ToUpper() == model.ProductName.ToUpper());
        //if (isProductExists)
        //{
        //  ModelState.AddModelError("Error", "Project with given project number already exists.");
        //  return BadRequest(ModelState);
        //}
        int id = await _salesService.AddEditBiscuitCash(model);
        model.Id = id;
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Post) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpGet("DeleteBiscuit/{id}")]
    public async Task<ActionResult> DeleteBiscuitbill(int id)
    {
      try
      {
        bool model = await _salesService.DeleteBiscuitBillById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
    [HttpGet("GetBiscuitBillByDate/{date}")]
    public async Task<ActionResult<List<BiscuitCash>>> GetBiscuitBillByDate(string date)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(date);
        List<BiscuitCash> model = await _salesService.GetBiscuitbillByDate(dt);
        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(SalesController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

  }
}
