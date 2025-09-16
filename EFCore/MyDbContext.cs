using Entites;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext>option): base(option)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Report> Reports { get; set; }
}
