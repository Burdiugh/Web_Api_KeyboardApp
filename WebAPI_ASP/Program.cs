
using Microsoft.EntityFrameworkCore;

using FluentValidation;
using FluentValidation.AspNetCore;

using BusinessLogicService.Middlewares;
using Infrastructure;
using Core.Validators;
using Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI_ASP;
using Microsoft.AspNetCore.Identity;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Castle.Core.Smtp;
using Core.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddSqlDbContext(builder.Configuration.GetConnectionString("AppKeyboardDbConnection"));
//AppKeyboardDbConnection
//AzureDbConnection

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity();
builder.Services.AddRepository();


builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddValidator();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"])),
    };
});


var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// seed
app.CreateMainAdminAsync();

app.UseStaticFiles();

app.UseCors(builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
);

app.UseHttpsRedirection();

app.UseMiddleware <ExceptionMiddleware>();

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.Run();
