using Cms.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

using CategoryEntity = Cms.Infrastructure.Entities.Category;
using CategoryDomain = Cms.Core.Domains.Category;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Data.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    public CmsDbContext _cmsDbContext { get; }
    public CategoryRepository(CmsDbContext cmsDbContext)
    {
      _cmsDbContext = cmsDbContext;
    }


    public async Task<int> Add(CategoryDomain category)
    {
      var dbModel = new CategoryEntity
      {
        CreatedDate = DateTime.Now,
        Title = category.Title
      };

      await _cmsDbContext.Categories.AddAsync(dbModel);

      await _cmsDbContext.SaveChangesAsync();

      return dbModel.Id;
    }


    public async Task Delete(int id)
    {
      var finded = await GetCategory(id);

      _cmsDbContext.Remove(finded);

      await _cmsDbContext.SaveChangesAsync();
    }

    public async Task Edit(CategoryDomain category)
    {
      var finded = await GetCategory(category.Id);

      finded.Title = category.Title;

      _cmsDbContext.Categories.Update(finded);

      await _cmsDbContext.SaveChangesAsync();
    }

    public async Task<List<CategoryDomain>> GetAll()
    {
      return await _cmsDbContext.Categories.Select(x => new CategoryDomain
      {
        Title = x.Title,
        CreatedDate = x.CreatedDate,
        Id = x.Id
      }).ToListAsync();
    }

    public async Task<CategoryDomain> GetById(int id)
    {
      var item = await _cmsDbContext.Categories.Select(x => new CategoryDomain
      {
        Id = x.Id,
        CreatedDate = x.CreatedDate
      }).FirstOrDefaultAsync(x => x.Id == id);


      if (item == null)
      {
        throw new Exception("Not Found");
      }

      return item;
    }

    private async Task<CategoryEntity> GetCategory(int id)
    {
      var item = await _cmsDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (item == null)
      {
        throw new Exception("Not Found");
      }
      return item;
    }
  }
}
