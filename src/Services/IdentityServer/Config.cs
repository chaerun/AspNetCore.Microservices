using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityAPI
{
  public class Config
  {
    private const string UnitApiScope = "unit.api";
    private const string EmployeeApiScope = "employee.api";

    public static IEnumerable<Client> Clients(IConfiguration configuration) => new Client[]
    {
      new Client
      {
        ClientId = "unit.client",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = { UnitApiScope }
      },
      new Client
      {
        ClientId = "employee.client",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = { EmployeeApiScope }
      },
      new Client
      {
        ClientId = "web.client",
        ClientName = "Web App",
        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
        AllowRememberConsent = false,
        RedirectUris = { configuration["WebAppSettings:RedirectUrl"] },
        PostLogoutRedirectUris = { configuration["WebAppSettings:PostLogoutRedirectUrl"] },
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedScopes = new List<string>
        {
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          UnitApiScope,
          EmployeeApiScope
        }
      }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
      {
        new ApiScope(UnitApiScope, "Unit API"),
        new ApiScope(EmployeeApiScope, "Employee API")
      };

    public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[]
      {
      };

    public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

    public static List<TestUser> TestUsers =>
      new List<TestUser>
      {
        new TestUser
        {
          SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
          Username = "admin",
          Password = "admin",
          Claims = new List<Claim>
          {
            new Claim(JwtClaimTypes.GivenName, "Adminstrator"),
            new Claim(JwtClaimTypes.FamilyName, "Web")
          }
        }
      };
  }
}
