using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.Commands
{
  public class PostAddCommand : IRequest<AddPostResponse>
  {
    public string Title { get; set; }
    public string Content { get; set; }
  }

  public class AddPostResponse
  {
    public int Id { get; set; }
  }
}
