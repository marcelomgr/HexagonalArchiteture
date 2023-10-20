using Application;
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
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddMediatR(typeof(PersonManager));

# region IoC
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
