using Cms.Core.UseCase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Core.ViewModels
{
  public class PostAddVm
  {
    [Required(ErrorMessage = "نام الزامی است.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "متن داخل پست الزامی است.")]
    public string Content { get; set; }

  }
  public class PostEditVm
  {
    [Required(ErrorMessage = "نام الزامی است.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "متن داخل پست الزامی است.")]
    public string Content { get; set; }

    [Required]
    public int Id { get; set; }

  }


}
