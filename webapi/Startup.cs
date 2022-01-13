using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
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
using webapi.DataLayer;
using webapi.Interfaces;
using webapi.Repository;
using webapi.Services;
using webapi.Interfaces.ServiceInterfaces;
using webapi.Communication;

namespace webapi
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //TODO: pogledati da li ovo treba ili transient
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IDeckService, DeckService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITerrainService, TerrainService>();
            services.AddScoped<IMageService, MageService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi", Version = "v1" });
            });
            services.AddCors(p => {
                p.AddPolicy("CORS", builder =>
                {
                    builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true).AllowCredentials();
                });
            });
            services.AddDbContext<BOMContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("BattleOfMagesDB"));
            });

            services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("messageHub");
            });
        }
    }
}
