using ApiProj.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string conn = Configuration.GetConnectionString("Main");

            services
                .AddDbContext<ClienteContext>(op => op
                .UseMySql(conn, ServerVersion.AutoDetect(conn)));

            services.AddScoped<ClienteContext, ClienteContext>();
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ClienteContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SetupDatabase(context);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetupDatabase(ClienteContext context)
        {
            context.Database.Migrate();

            if (!context.Clientes.Any())
            {
                var clientes = new List<Cliente>()
                {
                    new Cliente("João", 14, "997378028", "12123123000112"),
                    new Cliente("Carlos", 56, "998723158", "34345345000123"),
                    new Cliente("Rafaela", 01, "995698420", "67678678000134"),
                    new Cliente("Maria", 98, "990249652", "90901901000145")
                };
                context.Clientes.AddRange(clientes);
                context.SaveChanges();
            }
        }
    }
}
