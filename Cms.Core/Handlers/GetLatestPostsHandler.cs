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
  public class GetLatestPostsHandler : IRequestHandler<GetLatestPostsQuery, List<Post>>
  {
    public IPostRepository _postRepository { get; }

    public GetLatestPostsHandler(IPostRepository postRepository)
    {
      _postRepository = postRepository;
    }


    public async Task<List<Post>> Handle(GetLatestPostsQuery request, CancellationToken cancellationToken)
    {
      return await _postRepository.GetLatestPosts(20);
    }
  }
}
