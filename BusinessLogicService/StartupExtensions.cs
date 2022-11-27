using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static  class StartupExtensions
    {

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IScoreService, ScoreService>();
            services.AddScoped<ITextService, TextService>();
        }



        public static void AddValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddValidatorsFromAssemblyContaining<ScoreValidator>();
        }

        
    }
}
