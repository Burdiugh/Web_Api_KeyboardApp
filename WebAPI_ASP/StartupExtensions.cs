using Core;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Net;

namespace WebAPI_ASP
{
    public static class StartupExtensions
    {

     
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Keyboard API",
                    Version = "v1",
                    Description = "Keyboard API Services.",
                    Contact = new OpenApiContact
                    {
                        Name = "Burdiugh."
                    },
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }


        public static void CreateMainAdminAsync(this IApplicationBuilder applicationBuilder)
        {
           //var userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
           // var roleManager = applicationBuilder.ApplicationServices.GetRequiredService<RoleManager<AppRole>>();


            var scopeFactory = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();


               if (userManager.FindByEmailAsync("mainadmin@gmail.com").Result != null) { return; }


                var mainAdmin = new AppUser()
            {
                Email = "mainadmin@gmail.com",
                UserName = "MainAdmin",
            };

                    var result = userManager.CreateAsync(mainAdmin, "mainAdmin_1703").Result;

            if (!result.Succeeded)
            {
                string message = string.Join(' ', result.Errors.Select(e => e.Description));

                throw new HttpException(HttpStatusCode.BadRequest, message);
            }

             roleManager.CreateAsync(new IdentityRole("admin")).Wait();

             userManager.AddToRoleAsync(mainAdmin, "admin").Wait();

            }
        }
    }
}
