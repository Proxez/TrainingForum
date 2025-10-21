using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCore;

public class MyDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public MyDbContext(DbContextOptions<MyDbContext>option): base(option)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<User>(e =>
        {
            e.Property(x => x.FirstName).HasMaxLength(100);
            e.Property(x => x.LastName).HasMaxLength(100);
            e.Property(x => x.Address).HasMaxLength(200);
            e.Property(x => x.City).HasMaxLength(120);
            e.Property(x => x.UserName).HasMaxLength(40);
            e.Property(x => x.Email).HasMaxLength(320);
            e.Property(x => x.AvatarUrl).HasMaxLength(500);
            e.HasIndex(x => x.UserName).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

        b.Entity<Category>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(120);
            e.Property(x => x.Slug).HasMaxLength(140);
            e.HasIndex(x => x.Title).IsUnique();
            e.HasIndex(x => x.Slug).IsUnique();
        });

        b.Entity<SubCategory>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(140);
            e.Property(x => x.Slug).HasMaxLength(160);
            e.HasIndex(x => new { x.CategoryId, x.Title }).IsUnique();
            e.HasIndex(x => x.Slug).IsUnique();
            e.HasOne(x => x.Category).WithMany(c => c.SubCategories).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<Post>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(160);
            e.Property(x => x.ImageUrl).HasMaxLength(500);
            e.HasIndex(x => x.SubCategoryId);
            e.HasIndex(x => new { x.SubCategoryId, x.CreatedAt });
            e.HasQueryFilter(x => !x.IsDeleted);
            e.HasOne(x => x.User).WithMany(u => u.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.SubCategory).WithMany(sc => sc.Posts).HasForeignKey(x => x.SubCategoryId).OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<Comment>(e =>
        {
            e.HasIndex(x => x.PostId);
            e.HasIndex(x => new { x.PostId, x.CreatedAt });
            e.HasQueryFilter(x => !x.IsDeleted);
            e.HasOne(x => x.Post).WithMany(p => p.Comments).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany(u => u.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.ParentComment).WithMany(c => c.Comments).HasForeignKey(x => x.ParentCommentId).OnDelete(DeleteBehavior.Restrict);
        });
        b.Entity<Message>(e =>
        {
            e.Property(x => x.Body).HasMaxLength(4000).IsRequired();
            e.HasIndex(x => new { x.RecipientId, x.SentAtUtc });
            e.HasIndex(x => new { x.SenderId, x.RecipientId, x.SentAtUtc });

            e.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Recipient).WithMany().HasForeignKey(x => x.RecipientId).OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Reaction>(e =>
        {
            e.HasIndex(x => new { x.UserId, x.TargetType, x.TargetId, x.Type }).IsUnique();
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<Media>(e =>
        {
            e.HasOne(m => m.Post).WithMany(p => p.Media).HasForeignKey(m => m.PostId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            e.HasOne(m => m.Comment).WithMany(c => c.Media).HasForeignKey(m => m.CommentId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        });

        b.Entity<Report>(e =>
        {
            e.Property(x => x.Reason).HasMaxLength(500).IsRequired();
            e.HasOne(x => x.Reporter).WithMany().HasForeignKey(x => x.ReporterUserId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.ResolvedBy).WithMany().HasForeignKey(x => x.ResolvedByUserId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        });
    }
}
