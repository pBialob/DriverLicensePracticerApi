using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DriverLicensePracticerApi.Seeders;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Services;
using Microsoft.AspNetCore.Identity;
using DriverLicensePracticerApi.Models.Validators;
using DriverLicensePracticerApi.Models;
using System.Text;
using DriverLicensePracticerApi.Services.TestGenerator.Tests;
using DriverLicensePracticerApi.Repositories;
using Microsoft.EntityFrameworkCore;

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
            services.AddControllers();

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

            services.AddHealthChecks();
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DriverLicensePractierDb")));
            services.Configure<TestConfiguration>(Configuration.GetSection("TestConfiguration"));

            services.AddScoped<ApplicationMappingProfile>();
            services.AddAutoMapper(this.GetType().Assembly);

            services.AddScoped<QuestionSeeder>();
            services.AddScoped<CategorySeeder>();
            services.AddScoped<RoleSeeder>();

            services.AddHttpContextAccessor();

            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<QuestionQuery>, QuestionQueryValidator>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITestGeneratorService, TestGeneratorService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

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
