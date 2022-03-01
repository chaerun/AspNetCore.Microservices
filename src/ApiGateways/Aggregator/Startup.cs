using Aggregator.HttpHandlers;
using Aggregator.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Net.Mime;

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
      services.AddAutoMapper(typeof(Startup));

      services.AddScoped<IUnitService, UnitService>();
      services.AddScoped<IEmployeeService, EmployeeService>();
      services.AddTransient<AuthenticationDelegatingHandler>();

      services.AddHttpClient("unit.client", client =>
      {
        client.BaseAddress = new Uri(Configuration["ApiSettings:UnitUrl"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
      }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

      services.AddHttpClient("employee.client", client =>
      {
        client.BaseAddress = new Uri(Configuration["ApiSettings:EmployeeUrl"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
      }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

      services.AddHttpContextAccessor();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator", Version = "v1" });
      });

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
          options.Authority = Configuration["IdentityServerUrl"];
          options.RequireHttpsMetadata = false;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateAudience = false,
            ValidateIssuer = false
          };
        });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("ApiScopePolicy", policy => policy.RequireClaim("scope", "employee.api"));
      });
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
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
