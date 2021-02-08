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
  public interface IUsersService
  {
    Task<UserVewModel> AddEditUser(UserVewModel model);
    Task<List<UserVewModel>> GetUsers();
    Task<List<Role>> GetRoles();
    Task<UserVewModel> GetUserById(int userId);

  }
  public class UsersService : IUsersService
  {
    private readonly IMapper _mapper;
    private readonly ILogger<UsersService> _logger;
    private PumpDBContext _pumpDBContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    public UsersService(ILogger<UsersService> logger,
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
    public async Task<UserVewModel> AddEditUser(UserVewModel model)
    {
      try
      {
        if (model.Id == 0)
        {

          User user = new User()
          {

            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            PhoneNo = model.PhoneNo,
            Email = model.Email,
            UserName = model.UserName,
            Password = model.Password,
            Dob = model.Dob,
            RoleId = model.RoleId,
          };
          await _pumpDBContext.AddAsync(user);
          await _pumpDBContext.SaveChangesAsync();
          UserVewModel umodel = _mapper.Map<UserVewModel>(user);
          return umodel;
        }
        else
        {
          User user = await _pumpDBContext.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
          user.FirstName = model.FirstName;
          user.LastName = model.LastName;
          user.Address = model.Address;
          user.PhoneNo = model.PhoneNo;
          user.Email = model.Email;
          user.UserName = model.UserName;
          user.Password = model.Password;
          user.Dob = model.Dob;
          user.RoleId = model.RoleId;
          await _pumpDBContext.SaveChangesAsync();
          return _mapper.Map<UserVewModel>(user);
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<List<Role>> GetRoles()
    {
      try
      {
        List<Role> role = await _pumpDBContext.Roles.ToListAsync();
        return role;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<UserVewModel> GetUserById(int userId)
    {
      try
      {
        User user = await _pumpDBContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
        UserVewModel userVewModel = _mapper.Map<UserVewModel>(user);
        return userVewModel;
      }
      catch (Exception ex)
      {
        return null;
      }

    }

    public async Task<List<UserVewModel>> GetUsers()
    {
      try
      {
        List<User> user = await _pumpDBContext.Users.ToListAsync();
        List<UserVewModel> userVewModel = _mapper.Map<List<UserVewModel>>(user);
        return userVewModel;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
  }
}
