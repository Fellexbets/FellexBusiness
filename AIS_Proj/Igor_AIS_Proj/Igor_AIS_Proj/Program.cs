
using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IIdentityService, IdentityService>();


var jwtSettings = new JwtSettings();
builder.Configuration.Bind(key: nameof(jwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("AISProject")));

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
{
    Description = "JWT Authorization header using the bearer scheme",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
});
//x.AddSecurityRequirement(new OpenApiSecurityRequirement()
//      {
//        {
//          new OpenApiSecurityScheme
//          {
//            Reference = new OpenApiReference
//              {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//              },
//              Scheme = "oauth2",
//              Name = "Bearer",
//              In = ParameterLocation.Header,

//            },
//            new List<string>()
//          }
//        });
});


builder.Services.AddAuthentication(configureOptions: x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(configuration["Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
