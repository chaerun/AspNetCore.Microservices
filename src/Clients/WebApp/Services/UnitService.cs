using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public class UnitService : IUnitService
  {
    private readonly HttpClient _client;

    public UnitService(HttpClient client)
    {
      _client = client;
    }

    public Task<Unit> CreateUnitAsync(Unit dto)
    {
      throw new System.NotImplementedException();
    }

    public Task DeleteUnitAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<Unit> GetUnitByIdAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<Unit>> GetUnitsAsync()
    {
      var units = new List<Unit>()
      {
        new Unit()
        {
          Id = 1,
          Name = "Unit 1",
          Code = "u1",
          CreatedAt = DateTime.Now,
          CreatedBy = "System",
          IsActive = true
        },
        new Unit()
        {
          Id = 2,
          Name = "Unit 2",
          Code = "u2",
          CreatedAt = DateTime.Now,
          CreatedBy = "System",
          IsActive = true
        }
      };

      return await Task.FromResult(units);
    }

    public Task<Unit> UpdateUnitAsync(int id, Unit dto)
    {
      throw new System.NotImplementedException();
    }
  }
}

