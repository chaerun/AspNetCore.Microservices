using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mime;
using WebApp.HttpHandlers;
using WebApp.Services;

namespace WebApp
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
      services.AddControllersWithViews().AddRazorRuntimeCompilation();
      services.AddScoped<IUnitService, UnitService>();
      services.AddScoped<IEmployeeService, EmployeeService>();
      services.AddTransient<AuthenticationDelegatingHandler>();

      services.AddHttpClient("web.client", client =>
      {
        client.BaseAddress = new Uri(Configuration["OcelotApiGw"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(Microsoft.Net.Http.Headers.HeaderNames.Accept, MediaTypeNames.Application.Json);
      }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

      services.AddHttpContextAccessor();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
      })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
          options.Authority = Configuration["IdentityServerUrl"];
          options.RequireHttpsMetadata = false;

          options.ClientId = "web.client";
          options.ClientSecret = "secret";
          options.ResponseType = "code";

          options.Scope.Add("openid");
          options.Scope.Add("profile");
          options.Scope.Add("unit.api");
          options.Scope.Add("employee.api");

          options.SaveTokens = true;
          options.GetClaimsFromUserInfoEndpoint = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
