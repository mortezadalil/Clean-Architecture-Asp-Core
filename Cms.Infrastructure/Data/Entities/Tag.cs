using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Entities
{
  public class Tag
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }

    public IEnumerable<PostTag> PostTags { get; set; }

  }

  public class TagConfiguration : IEntityTypeConfiguration<Tag>
  {
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
      builder.ToTable("Tags");

      builder.HasKey(x => x.Id);

    }
  }
}
