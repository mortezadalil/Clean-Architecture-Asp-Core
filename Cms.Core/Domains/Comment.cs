using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.Domains
{
  public class Comment
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
  }
}
