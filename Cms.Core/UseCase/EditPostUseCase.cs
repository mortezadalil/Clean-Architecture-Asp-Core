using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cms.Core.Domains;
using Cms.Core.Dtos.General;
using Cms.Core.Dtos.UseCase;
using Cms.Core.Interfaces.Repository;

namespace Cms.Core.UseCase
{
  public class EditPostUseCase : IEditPostUseCase
  {
    public IPostRepository _postRepository { get; }
    public EditPostUseCase(IPostRepository postRepository)
    {
     _postRepository = postRepository;
    }


    public async Task HandleAsync(EditPostRequest message,
      IOutputPort<GenericResponse<EditPostResponse>> outputPort)
    {
      var post = new Post
      {
        Id = message.Id,
        Content = message.Content,
        Title = message.Title
      };

      outputPort.Handle(new GenericResponse<EditPostResponse> (
        new EditPostResponse { Id = await _postRepository.Edit(post) }));

  
    }
  }
}
