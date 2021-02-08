using AutoMapper;
using pumptally.Data.Entities;
using pumptally.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pumptally.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
     

      CreateMap<User, UserVewModel>();
      CreateMap<UserVewModel, User>();

      CreateMap<ProductViewModel, Product>();
      CreateMap<Product, ProductViewModel>();
    }
  }
}
