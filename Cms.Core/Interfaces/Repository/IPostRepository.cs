using Cms.Core.Commands;
using Cms.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Interfaces.Repository
{
  public interface IPostRepository
  {
    Task<List<Post>> GetLatestPosts(int count);
    Task<int> Add(PostAddCommand request);
    Task<int> Edit(Post post);
  }
}
