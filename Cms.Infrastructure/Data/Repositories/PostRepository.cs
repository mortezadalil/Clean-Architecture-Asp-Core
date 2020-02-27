using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Core.Commands;
using Cms.Core.Domains;
using Cms.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

using PostEntity=Cms.Infrastructure.Entities.Post;
using PostDomain = Cms.Core.Domains.Post;

namespace Cms.Infrastructure.Data
{
  public class PostRepository : IPostRepository
  {
    public CmsDbContext _cmsDbContext { get; }

    public PostRepository(CmsDbContext cmsDbContext)
    {
      _cmsDbContext = cmsDbContext;
    }


    public async Task<List<Post>> GetLatestPosts(int count)
    {
      return await _cmsDbContext.Posts
        .Select(x=>new Post
        {
          Id=x.Id,
          Title=x.Title,
          Content=x.Content,
          CreatedDate=x.CreatedDate
        }).OrderByDescending(x=>x.CreatedDate)
        .Take(count)
        .ToListAsync();
    }

    public async Task<int> Add(PostAddCommand request)
    {
      var item = new PostEntity
      {
        Content=request.Content,
        Title=request.Title
      };

      await _cmsDbContext.Posts.AddAsync(item);

      await _cmsDbContext.SaveChangesAsync();

      return item.Id;

    }

    public async Task<int> Edit(PostDomain post)
    {
      _cmsDbContext.Update(post);

      await _cmsDbContext.SaveChangesAsync();

      return post.Id;
    }
  }
}
