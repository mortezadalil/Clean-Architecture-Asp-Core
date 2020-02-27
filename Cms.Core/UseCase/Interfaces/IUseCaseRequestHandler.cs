using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.UseCase
{
  public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> 
    where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
  {
    Task HandleAsync(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
  }

  public interface IUseCaseRequest<out TUseCaseResponse> { }

  public interface IOutputPort<in TUseCaseResponse>
  {
    void Handle(TUseCaseResponse response);
  }

}
