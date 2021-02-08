using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pumptally.Data;
using pumptally.Services;
using pumptally.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pumptally
{
  public class Startup
  {
    public static string ConnectionString { get; private set; }
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      ConnectionString = Configuration.GetConnectionString("DefaultConnection");

      IConfigurationBuilder builder = new ConfigurationBuilder()
     .SetBasePath(env.ContentRootPath)
     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
     .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<PumpDBContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddSingleton<IConfiguration>(Configuration);
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddTransient<IUsersService, UsersService>();
      services.AddTransient<IProductService, ProductService>();
      services.AddTransient<ISalesService, SalesService>();


      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      //log service

      services.AddCors(options =>
      {
        var config = Configuration.GetSection("AllowSpecificOrigin").Get<OriginsModel>();
        string[] origins = config.Origins.Split(',');
        options.AddPolicy("AllowSpecificOrigin",
            builder1 => builder1.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader());
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "UCI API",
          Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Please insert JWT with Bearer into field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                               {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                               }
                          },
                          new string[] { }
                        }
                      });
      });

      services.AddAutoMapper(typeof(Startup));
      services.AddMvc();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();
      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UCI API");
        c.RoutePrefix = "swagger/ui";
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      loggerFactory.AddLog4Net();
      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("AllowSpecificOrigin");
      app.UseAuthentication();
      app.UseAuthorization();

      // Enable serving files from the configured web root folder (i.e., WWWROOT)
      app.UseStaticFiles();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
