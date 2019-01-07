using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamousIslands.Models;
using FamousIslands.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FamousIslands.Dtos;
using Swashbuckle.AspNetCore.Swagger;

namespace FamousIslands
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("FamousIslandsDBConnectionString")));

            services.AddScoped<IFamousIslandsRepository, FamousIslandsRepository>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            context.SeedDatabase();
            app.UseMvc();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<Country, CountryWithoutIslandDto>();
                cfg.CreateMap<Island, IslandDto>();
                cfg.CreateMap<IslandWithoutIdDto, Island>();
                cfg.CreateMap<IslandDto, Island>();
                
            });
        }
    }
}
 