using cargosiklink.Models;
using cargosiklink.Repository.Interfaces;
using cargosiklink.Repository;
using cargosiklink.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using cargosiklink.Service.Interfaces;
using cargosiklink.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDataContext>(opt => opt.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Index");
        opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Index");
    });


builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<NumberTrack>, NumberTrackRepository>();
builder.Services.AddScoped<IBaseRepository<State>, StateRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<INumberTrackService, NumberTrackService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
