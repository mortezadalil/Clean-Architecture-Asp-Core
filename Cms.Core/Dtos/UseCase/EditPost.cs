using Cms.Core.Dtos.General;
using Cms.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.Dtos.UseCase
{
  public class EditPostRequest : IUseCaseRequest<GenericResponse<EditPostResponse>>
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

  }

  public class EditPostResponse
  {
    public int Id { get; set; }
  }
}
