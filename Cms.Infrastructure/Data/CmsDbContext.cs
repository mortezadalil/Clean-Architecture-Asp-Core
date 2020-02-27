
using Cms.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Cms.Infrastructure.Data.Entities;

namespace Cms.Infrastructure.Data
{
  public class CmsDbContext : DbContext
  {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    public CmsDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new PostConfiguration());
      modelBuilder.ApplyConfiguration(new CommentConfiguration());
      modelBuilder.ApplyConfiguration(new TagConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new PostTagConfiguration());
      modelBuilder.ApplyConfiguration(new CategoryConfiguration());

    }
  }
  public class BloggingContextFactory : IDesignTimeDbContextFactory<CmsDbContext>
  {
    public CmsDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<CmsDbContext>();
      optionsBuilder.UseSqlServer(@"Server=DESKTOP-AKTCEF1;Database=CmsDb;Trusted_Connection=True;");


      return new CmsDbContext(optionsBuilder.Options);
    }
  }
}
