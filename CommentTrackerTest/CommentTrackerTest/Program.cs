using CommentTrackerTest.DAL;
using CommentTrackerTest.Hubs;
using CommentTrackerTest.Models;
using CommentTrackerTest.Subscription;
using CommentTrackerTest.Subscription.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
{
    policy.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(x => true);
}));

builder.Services.AddSingleton<DatabaseSubscription<About>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();

app.UseDatabaseSubscription<DatabaseSubscription<About>>("Abouts");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TestHub>("/TestHub");
});

app.MapControllerRoute(

    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"

    );

app.Run();
