using AnimalAdoption.Common.Domain;
using AnimalAdoption.Common.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoption.Web.Portal
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    private string _connectionString;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<Configuration>(Configuration);

      _connectionString = Configuration["SqlConnectionString"];

      services.AddDbContext<AnimalAdoptionContext>(options =>
      {
        options.UseSqlServer(_connectionString);
      });

      services.AddHttpContextAccessor();
      services.AddMemoryCache();
      services.AddTransient<AnimalService>();
      services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        //
        if (string.IsNullOrEmpty(_connectionString))
        {
          throw new Exception("Please make sure that you have set a SQL DB connection string...");
        }

        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHttpsRedirection();
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseStaticFiles();

      var failurePercentage = Configuration.GetValue<int?>("SimulatedFailureChance");
      if (failurePercentage != null)
      {
        var rand = new Random();
        app.Use(async (context, next) =>
        {
          if (failurePercentage <= rand.Next(0, 100))
          {
            await next.Invoke();
          }
          else
          {
            throw new Exception($"A simulated failure occurred - there is a {failurePercentage}% chance of this occurring");
          }
        });
      }

      if (string.IsNullOrWhiteSpace(_connectionString))
      {
        app.Use(async (context, next) =>
        {
          var url = context.Request.Path.Value;
          if (!url.ToLowerInvariant().Contains("/missingenvironmentvariable"))
          {
            // rewrite and continue processing
            context.Request.Path = "/missingenvironmentvariable";
          }

          await next();
        });
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
        endpoints.MapControllers();
      });
    }
  }
}
