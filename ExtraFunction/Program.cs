﻿using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ExtraFunction.Repository_.Interface;
using ExtraFunction.Repository_;
using ExtraFunction.Service_;
using ExtraFunction.DAL;
using ExtraFunction.Service;
using ExtraFunction.Authorization;
using ExtraFunction.Repository;
using System;
using ExtraFunction.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ExtraFunction
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                    .ConfigureFunctionsWorkerDefaults(Worker => Worker.UseNewtonsoftJson().UseMiddleware<JWTMiddleware>())
                    .ConfigureAppConfiguration(config =>
                         config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: false))
                    .ConfigureOpenApi()
                    .ConfigureServices(services =>
                    {
                        services.AddCors(options =>
                        {
                            options.AddPolicy(name: "MyAllowSpecificOrigins",
                                              builder =>
                                              {
                                                  builder.WithOrigins("*");
                                              });
                        });

                        services.AddControllers();
                        services.AddDbContext<DatabaseContext>(options =>
                                   options.UseCosmos(Environment.GetEnvironmentVariable("DBUri"),
                            Environment.GetEnvironmentVariable("DbKey"),
                            Environment.GetEnvironmentVariable("DbName")));
                        services.AddTransient<IUserService, UserService>();
                        services.AddTransient<IUserRepository, UserRepository>();
                        services.AddTransient<ILoginService, LoginService>();
                        services.AddTransient<ILoginRepository, LoginRepository>();
                        services.AddSingleton<ITokenService, TokenService>();
                        services.AddTransient<IAchievementRepository, AchievementRepository>();
                        services.AddTransient<IAchievementService, AchievementService>();
                        services.AddTransient<IDisclaimersService, DisclaimersService>();
                        services.AddTransient<IDisclaimersRepository, DisclaimersRepository>();
                        services.AddTransient<ITermsAndConditionsService, TermsAndConditionsService>();
                        services.AddTransient<ITermsAndConditionRepository, TermsAndConditionsRepository>();
                        services.AddTransient<IAdminRepository, AdminRepository>();
                        services.AddTransient<IAdminService, AdminService>();
                        services.AddTransient<IAdminLoginRepository, AdminLoginRepository>();
                        services.AddTransient<IAdminLoginService, AdminLoginService>();
                        services.AddTransient<IBubbleMessageRepository, BubbleMessageRepository>();
                        services.AddTransient<IBubbleMessageService, BubbleMessageService>();

                    })
                    .Build();
            await host.RunAsync();

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...
            app.UseCors("MyAllowSpecificOrigins");
            // ...
        }
    }
}
