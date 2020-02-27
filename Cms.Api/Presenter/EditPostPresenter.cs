using Cms.Core.Dtos.General;
using Cms.Core.Dtos.UseCase;
using Cms.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cms.Api.Presenter
{
  public class PostApiPresenter<T> : IOutputPort<GenericResponse<T>>
  {
    public JsonContentResult ContentResult { get; }

    public PostApiPresenter()
    {
      ContentResult = new JsonContentResult();
    }
    public void Handle(GenericResponse<T> response)
    {
      ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
      ContentResult.Content = ContentResult.Serialize(response.Success ? (object)response.Data : (object)response.Errors);
    }
  }
}
