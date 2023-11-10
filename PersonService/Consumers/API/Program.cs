using Application;
using Data.SqlServer;
using Application.Person;
using Domain.Person.Ports;
using Data.SqlServer.Person;
using Domain.ChangeLog.Ports;
using Application.PersonType;
using Domain.PersonType.Ports;
using Data.SqlServer.ChangeLog;
using Application.Person.Ports;
using Application.PersonGender;
using Data.SqlServer.PersonType;
using Domain.PersonGender.Ports;
using Data.SqlServer.PersonGender;
using Application.PersonType.Ports;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Application.PersonGender.Ports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

# region IoC
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IPersonTypeManager, PersonTypeManager>();
builder.Services.AddScoped<IPersonTypeRepository, PersonTypeRepository>();

builder.Services.AddScoped<IPersonGenderManager, PersonGenderManager>();
builder.Services.AddScoped<IPersonGenderRepository, PersonGenderRepository>();

builder.Services.AddScoped<IChangeLogRepository, ChangeLogRepository>();
# endregion

# region DB wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<GdlDbContext>(
    options => options.UseSqlServer(connectionString));
# endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
