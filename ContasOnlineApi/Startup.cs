using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContasOnlineModel.Interfaces;
using ContasOnlineApi.Services;
using MongoDB.Driver;
using ContasOnlineModel.Modelo;

namespace ContasOnlineApi
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

            services.AddMvc();

            //Configurar o context do MongoBD
            services.Configure<MongoDBSettings>(Options =>
            {
                Options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                Options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            //Configuracao dos Onesignal
            services.Configure<OneSignalSettings>(Options =>
            {
                Options.ApiKey = Configuration.GetSection("OneSignal:ApiKey").Value;
                Options.RestKey = Configuration.GetSection("OneSignal:RestKey").Value;
                Options.RestUrl = Configuration.GetSection("OneSignal:RestUrl").Value;
            });

            
            //Configura os Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
                        
        }
    }
}
