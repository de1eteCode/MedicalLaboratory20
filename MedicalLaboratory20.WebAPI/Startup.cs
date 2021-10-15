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

namespace MedicalLaboratory20.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion

            services.AddControllers().AddJsonOptions(opt =>
            {
                // Отключает рекурсию в json ссылках
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
