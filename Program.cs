using me_faz_um_pix.Data;
using me_faz_um_pix.Middlewares;
using me_faz_um_pix.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using me_faz_um_pix.Config;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true
  };
});

// configs
IConfigurationSection jwtConfigurationSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<TokenConfig>(jwtConfigurationSection);

// database
builder.Services.AddDbContext<AppDBContext>(opts =>
{
    string host = builder.Configuration["Database:Host"] ?? string.Empty;
    string port = builder.Configuration["Database:Port"] ?? string.Empty;
    string username = builder.Configuration["Database:Username"] ?? string.Empty;
    string database = builder.Configuration["Database:Name"] ?? string.Empty;
    string password = builder.Configuration["Database:Password"] ?? string.Empty;

    string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
    opts.UseNpgsql(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<HealthService>();
builder.Services.AddScoped<KeyService>();
builder.Services.AddScoped<TokenService>();

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

// Middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
