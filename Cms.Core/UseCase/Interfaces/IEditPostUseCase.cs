using Cms.Core.Dtos.General;
using Cms.Core.Dtos.UseCase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.UseCase
{
  public interface IEditPostUseCase : IUseCaseRequestHandler<EditPostRequest,GenericResponse<EditPostResponse>>
  {
  }
}
