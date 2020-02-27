using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Entities
{
  public class Category 
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }

  }
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.ToTable("Categories");

      builder.HasKey(x => x.Id);

      builder.Property(p => p.Title)
        .HasMaxLength(50);
    }
  }
}
