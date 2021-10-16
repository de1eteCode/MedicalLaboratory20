using DataAccess.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using DataModels.Interfaces;
using DataAccess.EFCore.Repositories;
using DataModels.Interfaces.IEntityRepositories;
using DataAccess.EFCore.Repositories.EntityRepositories;
using DataAccess.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DataModels.Interfaces.IUnitOfWorks;
using MedicalLaboratory20.WebAPI.Helpers;

namespace MedicalLaboratory20.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // EF Core
            services.AddDbContext<LaboratoryContext>(option =>
            {
                option.UseLazyLoadingProxies();
                option.UseSqlServer(Configuration["ConnectionStrings:localhost"], opt =>
                {
                    opt.MigrationsAssembly(typeof(LaboratoryContext).Assembly.FullName);
                });
            });

            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IInsuranceRepository, InsuranceRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderServiceRepository, OrderServiceRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<ISafetyRepository, SafetyRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<ISocialRepository, SocialRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserServiceRepository, UserServiceRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUnitOfAuthUser, UnitOfAuthUser>();

            #if DEBUG
            services.AddTransient<IUnitRoot, UnitRoot>();
            #endif

            #endregion

            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    // Отключает рекурсию в json ссылках
                    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });

            #region Auth

            // Настройки
            var authOptionsRegistr = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsRegistr);

            // Сама аунтетификация
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            #endregion

            services.AddSignalR();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "My API V1");
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
