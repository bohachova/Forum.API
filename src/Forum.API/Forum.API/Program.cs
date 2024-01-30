using Forum.API.BL.Handlers;
using Forum.API.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Forum.API.BL.Security;
using Forum.API.BL.Abstracts;
using Forum.API.BL.Services;
using Forum.API.BL.Configuration;
using Forum.API.BL.Configuration.Interfaces;
using Microsoft.Extensions.Options;
using System.Reflection;
using Forum.API.BL.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AuthorizationHandler).Assembly));
builder.Services.Configure<FileValidationConfiguration>(builder.Configuration.GetSection("FileUploadSettings"));
builder.Services.AddScoped<IFileValidationConfiguration>(x => x.GetRequiredService<IOptions<FileValidationConfiguration>>().Value);
builder.Services.AddForumApiDbContext(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JWTAuthOptions.ISSUER,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = JWTAuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));

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

