using Cms.Core.Commands;
using Cms.Core.Commands;
using Cms.Core.Domains;
using Cms.Core.Interfaces.Repository;
using Cms.Core.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cms.Core.Handlers
{
  public class AddPostHandler : IRequestHandler<PostAddCommand, AddPostResponse>
  {
    public IPostRepository _postRepository { get; }

    public AddPostHandler(IPostRepository postRepository)
    {
      _postRepository = postRepository;
    }

    public async Task<AddPostResponse> Handle(PostAddCommand request, CancellationToken cancellationToken)
    {
      return new AddPostResponse { Id = await _postRepository.Add(request) };
    }
  }
}
