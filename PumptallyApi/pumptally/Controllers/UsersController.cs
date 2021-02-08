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
  public class UsersController : ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly PumpDBContext _pumpDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    private IUsersService _userService;

    public UsersController(IUsersService userService, ILogger<UsersController> logger,
        IConfiguration config, PumpDBContext pumpDbContext, IWebHostEnvironment hostingEnvironment)
    {
      _userService = userService;
      _logger = logger;
      _config = config;
      _pumpDbContext = pumpDbContext;
      _webHostEnvironment = hostingEnvironment;
    }

    /// <summary>
    /// Get Users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<UserVewModel>>> Get()
    {
      try
      {
        List<UserVewModel> users = await _userService.GetUsers();
        return Ok(users);
      }
      catch (Exception ex)
      {
        _logger.LogError("[" + nameof(UsersController) + "." + nameof(Get) + "]" + ex);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

  }
}
