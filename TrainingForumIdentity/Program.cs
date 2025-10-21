using Application.Service;
using Application.Service.Interface;
using EFCore;
using EFCore.Clients;
using Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;
using TrainingForumIdentity.Areas.Identity.Pages.Account.Manage;

namespace TrainingForumIdentity;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("MyDbContext") ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.");
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<ICategoryRepository, CategoryAPIRepository>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<ICommentRepository, CommentRepository>();
        builder.Services.AddTransient<ICommentService, CommentService>();
        builder.Services.AddTransient<IMediaRepository, MediaRepository>();
        builder.Services.AddTransient<IMediaService, MediaService>();
        builder.Services.AddTransient<IMessageRepository, MessageRepository>();
        builder.Services.AddTransient<IMessageService, MessageService>();
        builder.Services.AddTransient<IPostRepository, PostRepository>();
        builder.Services.AddTransient<IPostService, PostService>();
        builder.Services.AddTransient<IReactionRepository, ReactionRepository>();
        builder.Services.AddTransient<IReactionService, ReactionService>();
        builder.Services.AddTransient<IReportRepository, ReportRepository>();
        builder.Services.AddTransient<IReportService, ReportService>();
        builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
        //builder.Services.AddTransient<ISubCategoryRepository, SubCategoryAPIRepository>();
        builder.Services.AddTransient<ISubCategoryService, SubCategoryService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();

        builder.Services.AddTransient<IEmailSender, EmailSender>();

        builder.Services.AddHttpClient<ICategoryClient, CategoryClient>(options =>
        {
            options.BaseAddress = new Uri("https://proxeztrainingforumapi.azurewebsites.net/");
        });

        builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<MyDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.WebHost.UseWebRoot("wwwroot");


        var app = builder.Build();
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        Console.WriteLine("UNHANDLED: " + e.ExceptionObject);        

        // Logga alla requests så vi ser om POST:en kommer fram
        app.Use(async (ctx, next) =>
        {
            Console.WriteLine($"REQ  {ctx.Request.Method} {ctx.Request.Path}");
            await next();
            Console.WriteLine($"RESP {ctx.Response.StatusCode} {ctx.Request.Method} {ctx.Request.Path}");
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            //app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseStatusCodePages();

        app.UseAuthentication();
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
