using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ColegioApp.Models;

namespace ColegioApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddDbContext<ColegioContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=ColegioAppDB;User Id=user1;Password=123",ServerVersion.Create(new Version (8,0,33), ServerType.MySql));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Seed the database with initial data
            SeedData.Initialize(app.ApplicationServices);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Alumno",
                    pattern: "Alumno/{action=Index}/{id?}",
                    defaults: new { controller = "Alumno" });

                endpoints.MapControllerRoute(
                    name: "Grado",
                    pattern: "Grado/{action=Index}/{id?}",
                    defaults: new { controller = "Grado" });

                endpoints.MapControllerRoute(
                    name: "Profesor",
                    pattern: "Profesor/{action=Index}/{id?}",
                    defaults: new { controller = "Profesor" });

                endpoints.MapControllerRoute(
                    name: "AlumnoGrado",
                    pattern: "AlumnoGrado/{action=Index}/{id?}",
                    defaults: new { controller = "AlumnoGrado" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

            });
        }
    }
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ColegioContext>();

                SeedProfesores(dbContext);
                SeedGrados(dbContext);
                SeedAlumnos(dbContext);
                SeedAlumnoGrados(dbContext);
            }
        }

        private static void SeedProfesores(ColegioContext context)
        {
            if (!context.Profesores.Any())
            {
                var profesores = new List<Profesor>
                {
                    new Profesor { Nombre = "Teacher1", Apellidos = "Lastname1", Genero = "Male" },
                    new Profesor { Nombre = "Teacher2", Apellidos = "Lastname2", Genero = "Female" },
                };

                context.Profesores.AddRange(profesores);
                context.SaveChanges();
            }
        }

        private static void SeedGrados(ColegioContext context)
        {
            if (!context.Grados.Any())
            {
                var grados = new List<Grado>
                {
                    new Grado { Nombre = "Primero", ProfesorID = 1 }, // Ensure Profesor with ID 1 exists
                    new Grado { Nombre = "Segundo", ProfesorID = 2 }, // Ensure Profesor with ID 2 exists
                };

                context.Grados.AddRange(grados);
                context.SaveChanges();
            }
        }

        private static void SeedAlumnos(ColegioContext context)
        {
            if (!context.Alumnos.Any())
            {
                var alumnos = new List<Alumno>
                {
                    new Alumno { Nombre = "Student1", Apellidos = "Lastname1", Genero = "Male", FechaNacimiento = new DateTime(2000, 1, 1) },
                    new Alumno { Nombre = "Student2", Apellidos = "Lastname2", Genero = "Female", FechaNacimiento = new DateTime(2000, 2, 2) },
                };

                context.Alumnos.AddRange(alumnos);
                context.SaveChanges();
            }
        }

        private static void SeedAlumnoGrados(ColegioContext context)
        {
            if (!context.AlumnoGrados.Any())
            {
                // First, create Grado records if not already created
                SeedGrados(context);

                var alumnoGrados = new List<AlumnoGrado>
                {
                    new AlumnoGrado { AlumnoID = 1, GradoID = 2, Seccion = "A" }, // Ensure Alumno and Grado with IDs 1 exist
                    new AlumnoGrado { AlumnoID = 2, GradoID = 3, Seccion = "B" }, // Ensure Alumno and Grado with IDs 2 exist
                };

                context.AlumnoGrados.AddRange(alumnoGrados);
                context.SaveChanges();
            }
        }

    }

}
