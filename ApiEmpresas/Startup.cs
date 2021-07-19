// Unused usings removed
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Api.Models;

using System;
using System.Reflection;
using System.IO;

using Microsoft.OpenApi.Models;

namespace TodoApi
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
            //services.AddDbContext<ContextoDB>(opt =>
               //opt.UseInMemoryDatabase("Lista"));
            services.AddDbContext<ContextoDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("FormacionDB")));

            services.AddCors();

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Empresas",
                    Description = "API de ejemplo",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            var scopeeee = app.ApplicationServices.CreateScope();
            var context = scopeeee.ServiceProvider.GetRequiredService<ContextoDB>();
            //AddTestData(context);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(ContextoDB context)
        {
            //add fake data
            Empresa mercadona = new Empresa
            {
                NombreSociedad = "MERCADONA SA",
                Cif = "A46103834",
                CoincidenciaPor =  "Acrónimo",
                TextoCoincidencia = "MERCADONA",
                Provincia = "VALENCIA",
                NombreFiltrado = "MERCADONA_SA",
                LongitudBarra = 100,
                DatosRegistrales = "Vigente"
            };

            Empresa natural = new Empresa
            {
                NombreSociedad = "MERCADO NATURAL DOS HERMANAS SOCIEDAD LIMITADA",
                Cif = "B91632596",
                CoincidenciaPor = "Denominación social",
                TextoCoincidencia = "MERCADO NATURAL DOS HERMANAS SOCIEDAD LIMITADA",
                Provincia = "SEVILLA",
                NombreFiltrado = "MERCADO_NATURAL_DOS_HERMANAS_SOCIEDAD_LIMITADA",
                LongitudBarra = 68,
                DatosRegistrales = "Vigente"
            };

            Empresa german = new Empresa
            {
                NombreSociedad = "GERMAN VIZCAINO SOCIEDAD LIMITADA",
                Cif = "B74249160",
                CoincidenciaPor = "Marca",
                TextoCoincidencia = "MERCADO NAVIDEÑO XIXON MENAX MERCAU",
                Provincia = "ASTURIAS",
                NombreFiltrado = "GERMAN_VIZCAINO_SOCIEDAD_LIMITADA",
                LongitudBarra =  35,
                DatosRegistrales = "Vigente"
            };

            context.EmpresasPAG.Add(mercadona);
            context.EmpresasPAG.Add(natural);
            context.EmpresasPAG.Add(german);
            context.SaveChanges();
        }
    }
}