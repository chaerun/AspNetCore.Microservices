using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityAPI
{
  public class Config
  {
    private const string UnitApiScope = "unit.api";
    private const string EmployeeApiScope = "employee.api";

    public static IEnumerable<Client> Clients =>
      new Client[]
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
      };

    public static List<TestUser> TestUsers =>
      new List<TestUser>
      {
      };
  }
}
