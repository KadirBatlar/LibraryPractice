using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationApp.FluentValidators;
using FluentValidationApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Customer modeli için özel bir Validator servisini ekle
//builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    // Assembly içindeki tüm IValidator implementasyonlarýný otomatik olarak kaydet
    options.RegisterValidatorsFromAssemblyContaining<Program>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});

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

app.Run();
