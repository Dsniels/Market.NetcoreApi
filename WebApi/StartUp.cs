﻿using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using WebApi.DTO;
using WebApi.Middleware;

namespace WebApi
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }


        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<MarketDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<SeguridadDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
            });

            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddControllers();
            services.AddCors(opt => opt.AddPolicy("CorsRule", rule => rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"))) ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

            app.UseRouting();

            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
