using Microsoft.EntityFrameworkCore;
using Noman.Models;
using Noman.Respository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = builder.Configuration.GetConnectionString("dbcs");
builder.Services.AddDbContext<StudentDbContext>(database => database.UseSqlServer(config));
builder.Services.AddScoped<IStudent,StudentRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
