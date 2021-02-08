
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using pumptally.Data;
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
  public class ProductController : ControllerBase
  {
    private readonly ILogger<ProductController> _logger;
    private readonly PumpDBContext _pumpDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    private IProductService _productService;

    public ProductController(IProductService productService, ILogger<ProductController> logger,
        IConfiguration config, PumpDBContext pumpDbContext, IWebHostEnvironment hostingEnvironment)
    {
      _productService = productService;
      _logger = logger;
      _config = config;
      _pumpDbContext = pumpDbContext;
      _webHostEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductViewModel>>> Get()
    {
      try
      {
        List<ProductViewModel> prods = await _productService.GetProduct();
        return Ok(prods);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(ProductController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> Get(int id)
    {
      try
      {
        ProductViewModel product = await _productService.GetProductById(id);
        if (product == null)
          return NotFound();

        return Ok(product);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(ProductController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductViewModel model)
    {
      if (!IsModelValid(model))
      {
        return BadRequest();
      }
      try
      {
        //show error is project is already present with given project name
        if(model.Id  == 0)
        {
          bool isProductExists = _pumpDbContext.Products.Any(x => x.ProductName.ToUpper() == model.ProductName.ToUpper() && x.CodeNo == model.CodeNo);
          if (isProductExists)
          {
            ModelState.AddModelError("Error", "Project with given project number already exists.");
            return BadRequest(ModelState);
          }
        }
        
        int productId = await _productService.AddEditProduct(model);
        model.Id = productId;
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(ProductController) + "." + nameof(Post) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpGet("DeleteProduct/{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
      try
      {
        bool model = await _productService.DeleteProductById(id);
        if (model == null)
          return NotFound();

        return Ok(model);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(ProductController) + "." + nameof(DeleteProduct) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }


    private bool IsModelValid(ProductViewModel model)
    {
      bool flag = true;
      if (model.ProductName == "" || model.ProductName == null)
      {
        flag = false;
      }
      return flag;
    }
  }
}
