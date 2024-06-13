using WeeklyProgram;
using WeeklyProgram.Extensions;
using Microsoft.Extensions.DependencyInjection;
using WeeklyProgram.Data;
using WeeklyProgram.Models;
using WeeklyProgram.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ImageService>();

builder.Services.ConfigureDbContext(builder.Configuration);

var app = builder.Build();

#region  seedData configuration settings
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var context = services.GetRequiredService<ApplicationContext>();
//         SeedData.Initialize(services);

//     }
//     catch (Exception ex)
//     {

//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "An error occurred while seeding the database.");
//     }
// }
#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
