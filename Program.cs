using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parcial1.Data;
using Parcial1.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VideojuegoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VideojuegoContext") ?? throw new InvalidOperationException("Connection string 'VideojuegoContext' not found.")));
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>()
//Podemos crear usuarios y roles
    .AddRoles<IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VideojuegoContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapRazorPages();

app.Run();
