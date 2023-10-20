using MVC;
using Application;
using AutoMapper;
using MVC.Mapper;
using Data.SqlServer;
using Application.Person;
using Domain.Person.Ports;
using Data.SqlServer.Person;
using Application.PersonType;
using Domain.PersonType.Ports;
using Application.Person.Ports;
using Data.SqlServer.PersonType;
using Application.PersonType.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

# region IoC
//builder.Services.AddScoped<IPersonManager, PersonManager>();
//builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonTypeManager, PersonTypeManager>();
builder.Services.AddScoped<IPersonTypeRepository, PersonTypeRepository>();
# endregion

# region DB wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<GdlDbContext>(
    options => options.UseSqlServer(connectionString));
# endregion

//builder.UseStartup<Startup>();

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
