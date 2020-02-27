using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Core.Domains;
using Cms.Core.Interfaces.Repository;
using Cms.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cms.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController : BaseController
  {
    public ICategoryRepository _categoryRepository { get; }

    public CategoryController(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
      try
      {
        var category = await _categoryRepository.GetById(id);
        return CustomOk(category);

      }
      catch (Exception ex)
      {

        return CustomError(ex.Message);
      }

    }


    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
      try
      {
        var category = await _categoryRepository.GetAll();
        return CustomOk(category);

      }
      catch (Exception ex)
      {

        return CustomError(ex.Message);
      }

    }


    [HttpPost]
    public async Task<IActionResult> Post(CategoryAddVm category)
    {
      try
      {
        var id = await _categoryRepository.Add(new Category
        {
          Title = category.Title
        });

        return CustomOk(id);
      }
      catch (Exception ex)
      {

        return CustomError(ex.Message);

      }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CategoryEditVm category)
    {

      if (id != category.Id)
      {
        return CustomError();
      }

      try
      {
        await _categoryRepository.Edit(new Category
        {
          Id = id,
          Title = category.Title
        });

        return CustomOk(true);
      }
      catch (Exception ex)
      {
        return CustomError(ex.Message);
      }

    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _categoryRepository.Delete(id);
        return CustomOk(true);
      }
      catch (Exception ex)
      {

        return CustomError(ex.Message);

      }

    }

  }
}