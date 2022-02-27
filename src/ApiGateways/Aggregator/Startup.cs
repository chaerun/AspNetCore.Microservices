using Aggregator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace Aggregator
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpClient<IUnitService, UnitService>(c => c.BaseAddress = new Uri(Configuration["ApiSettings:UnitUrl"]));
      services.AddHttpClient<IEmployeeService, EmployeeService>(c => c.BaseAddress = new Uri(Configuration["ApiSettings:EmployeeUrl"]));
      services.AddAutoMapper(typeof(Startup));

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator", Version = "v1" });
      });

      services.AddHttpContextAccessor();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator v1"));
      }

      app.UseSerilogRequestLogging(o =>
      {
        o.MessageTemplate =
          "HTTP {RequestMethod} {RequestPath} from {ClientIp} ({ClientAgent}) responded {StatusCode} in {Elapsed:0.0000} ms";
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
