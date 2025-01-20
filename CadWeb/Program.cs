using CadWeb.Context;
using CadWeb.Models;
using CadWeb.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração para validação customizada (FluentValidation).
builder.Services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
builder.Services.AddScoped<IValidator<EstudanteViewModel>, EstudanteValidator>();

// Configuração para trabalhar com HttpContext.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Configuração do provider SQL-Server. Também configurado o tipo de carregamento LazyLoading.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
           .UseLazyLoadingProxies();
});

// Configuração do client para envio de requisição.
builder.Services.AddHttpClient();

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

app.UseSession(); //Para utilizar o HttpContextAccessor.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
