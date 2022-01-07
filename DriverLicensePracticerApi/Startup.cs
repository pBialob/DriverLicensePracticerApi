using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverLicensePracticerApi.Seeders;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Services;
using DriverLicensePracticerApi.Services.TestGenerator;
using Microsoft.AspNetCore.Identity;
using DriverLicensePracticerApi.Models.Validators;
using DriverLicensePracticerApi.Models;
using System.Text;

namespace DriverLicensePracticerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            object p = services.AddControllers().AddFluentValidation();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DriverLicensePracticerApi", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<ApplicationMappingProfile>();
            services.AddAutoMapper(this.GetType().Assembly);

            services.AddScoped<QuestionSeeder>();
            services.AddScoped<CategorySeeder>();
            services.AddScoped<RoleSeeder>();

            services.AddHttpContextAccessor();

            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<IAccountService, AccountService>();  
            services.AddScoped<TestFactory>();
            services.AddScoped<ITestService, TestService>();    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CategorySeeder categorySeeder, QuestionSeeder questionSeeder, RoleSeeder roleSeeder)
        {
            questionSeeder.Seed("questionsBase.xlsx");
            categorySeeder.Seed();
            roleSeeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DriverLicensePracticerApi v1"));
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
