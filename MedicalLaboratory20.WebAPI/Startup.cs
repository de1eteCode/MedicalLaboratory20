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
using MedicalLaboratory20.WebAPI.JWT;
using System.Text;
using MedicalLaboratory20.WebAPI.Models;

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
                option.UseSqlServer(Configuration["ConnectionStrings:pk"], opt =>
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
            #region Auth

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidateAudience = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddHostedService<JwtRefreshTokenCache>();

            #endregion

            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    // Отключает рекурсию в json ссылках
                    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });

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
