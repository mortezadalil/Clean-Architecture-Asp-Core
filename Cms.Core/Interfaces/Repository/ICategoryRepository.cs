using Cms.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Interfaces.Repository
{
  public interface ICategoryRepository
  {
    Task<Category> GetById(int id);
    Task<List<Category>> GetAll();
    Task<int> Add(Category category);
    Task Edit(Category category);
    Task Delete(int id);

  }
}
