using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Entities
{
  public class Comment
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
  }

  public class CommentConfiguration : IEntityTypeConfiguration<Comment>
  {
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
      builder.ToTable("Comments");

      builder.HasKey(x => x.Id);


      builder.Property(p => p.Content)
        .HasMaxLength(1000);


      builder.HasOne(e => e.Post).WithMany(c => c.Comments);

    }
  }
}
