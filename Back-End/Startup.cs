using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Back_End.Repositorio;
using Back_End.Entidades;
using Back_End.DTOs;

using System.Text.Json.Serialization;

namespace Back_End
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
            //datos
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))
            );

            //automaper    
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Autores, AutoresDTO>().ReverseMap();
                configuration.CreateMap<Autores, AutoresCreaDTO>().ReverseMap();
                configuration.CreateMap<Generos, GenerosDTO>().ReverseMap();
                configuration.CreateMap<Libros, LibrosDTO>().ReverseMap();
                configuration.CreateMap<Configuraciones, ConfiguracionesDTO>().ReverseMap();
            }, typeof(Startup));

            //regisro de interfaz y repositorio, servicios
            services.AddTransient<IAutoresRep, AutoresRep>();
            services.AddTransient<ILibrosRep, LibrosRep>();
            services.AddTransient<IGenerosRep, GenerosRep>();
            services.AddTransient<IConfiguracionesRep, ConfiguracionesRep>();

            //interfaz para api
            services.AddControllers();         

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Back_End", Version = "v1" });
            });

            //serializacion de objetos respuesta
            services.AddControllers().AddJsonOptions(x =>
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Back_End v1"));
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
