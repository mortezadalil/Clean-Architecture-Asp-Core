using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Entities
{
  public class PostTag
  {
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
  }

  public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
  {
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
      builder.ToTable("Posts_Tags");

      builder.HasKey(x => x.Id);


      builder.HasOne(e => e.Tag).WithMany(c => c.PostTags);
      builder.HasOne(e => e.Post).WithMany(c => c.PostTags);

    }
  }
}
