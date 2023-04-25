using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneVOne.GameService.Infrastructure;
using OneVOne.GameService.Repository;
using PlayerData.WebAPI.Options;
using PlayerData.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerData.WebAPI
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
            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<UnitOfWorkOptions>(Configuration.GetSection("UnitOfWork"));

            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetSection("UnitOfWork:ConnectionString").Value));

            services.AddScoped<UnitOfWork>().AddScoped<IUnitOfWork, UnitOfWork>(s => s.GetService<UnitOfWork>());
            services.Configure<PersonDbOptions>(Configuration.GetSection("PersonDb"));
            services.Configure<PersonsSqlOptions>(Configuration.GetSection("PersonsSql"));

            services.AddScoped<PersonsDBChromeDriverService>()
                .AddScoped<IPersonsDBChromeDriverService, PersonsDBChromeDriverService>(s => s.GetService<PersonsDBChromeDriverService>());
            services.AddSwaggerGen();

            services.AddScoped<PlayerStatsToDBChromeDriverService>()
                .AddScoped<IPlayerStatsToDBChromeDriverService, PlayerStatsToDBChromeDriverService>(s => s.GetService<PlayerStatsToDBChromeDriverService>());
            services.AddSwaggerGen();
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
