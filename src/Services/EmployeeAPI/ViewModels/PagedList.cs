using System.Collections.Generic;

namespace EmployeeAPI.ViewModels
{
  public class PagedList<TEntity> where TEntity : class
  {
    public bool Success { get; private set; } = true;
    public string Message { get; private set; } = string.Empty;
    public long TotalData { get; private set; }
    public IEnumerable<TEntity> Data { get; private set; }

    public PagedList(bool success, string message, long totalData, IEnumerable<TEntity> data)
    {
      Success = success;
      Message = message;
      TotalData = totalData;
      Data = data;
    }
  }
}
