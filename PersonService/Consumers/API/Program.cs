using API;
using System.Text;
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
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Application.PersonGender.Ports;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

# region Identity Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GdlDbContext>()
    .AddDefaultTokenProviders();
#endregion

# region Jwt
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appSettings.ValidIn,
        ValidIssuer = appSettings.Issuer
    };
});
#endregion

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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();