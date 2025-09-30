using Application.Service;
using Application.Service.Interface;
using EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TrainingForumIdentity;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("MyDbContext") ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.");
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<ICommentRepository, CommentRepository>();
        builder.Services.AddTransient<ICommentService, CommentService>();
        builder.Services.AddTransient<IMediaRepository, MediaRepository>();
        builder.Services.AddTransient<IMediaService, MediaService>();
        builder.Services.AddTransient<IPostRepository, PostRepository>();
        builder.Services.AddTransient<IPostService, PostService>();
        builder.Services.AddTransient<IReactionRepository, ReactionRepository>();
        builder.Services.AddTransient<IReactionService, ReactionService>();
        builder.Services.AddTransient<IReportRepository, ReportRepository>();
        builder.Services.AddTransient<IReportService, ReportService>();
        builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
        builder.Services.AddTransient<ISubCategoryService, SubCategoryService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MyDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
