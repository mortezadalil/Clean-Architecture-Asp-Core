using Cms.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.Queries
{
  public class GetLatestPostsQuery : IRequest<List<Post>>
  {

  }
}
