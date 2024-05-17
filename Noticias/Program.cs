using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using Noticias.Helpers;
using Noticias.Models.Entities;
using Noticias.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{

    var issuer = builder.Configuration.GetSection("JwtBearer").GetValue<string>("Issuer");
    var audience = builder.Configuration.GetSection("JwtBearer").GetValue<string>("Audience");
    var secret = builder.Configuration.GetSection("JwtBearer").GetValue<string>("Secret");


    x.TokenValidationParameters = new()
    {
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret??"")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

    };

});
var connection = builder.Configuration.GetConnectionString("noticias");
builder.Services.AddDbContext<ItesrcneOctavoContext>(x => x.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
