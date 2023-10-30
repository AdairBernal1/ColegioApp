using ColegioApp.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ColegioApp
{
    public class ColegioContext : DbContext
    {
        public ColegioContext(DbContextOptions<ColegioContext> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<AlumnoGrado> AlumnoGrados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Alumno entity
            modelBuilder.Entity<Alumno>()
                .HasKey(a => a.Id);

            // Configure Profesor entity
            modelBuilder.Entity<Profesor>()
                .HasKey(p => p.Id);

            // Configure Grado entity
            modelBuilder.Entity<Grado>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<Grado>()
                .HasOne(g => g.Profesor)
                .WithMany()
                .HasForeignKey(g => g.ProfesorID);

            // Configure AlumnoGrado entity
            modelBuilder.Entity<AlumnoGrado>()
                .HasKey(ag => ag.Id);
            modelBuilder.Entity<AlumnoGrado>()
                .HasOne(ag => ag.Alumno)
                .WithMany()
                .HasForeignKey(ag => ag.AlumnoID);
            modelBuilder.Entity<AlumnoGrado>()
                .HasOne(ag => ag.Grado)
                .WithMany()
                .HasForeignKey(ag => ag.GradoID);
        }
    }
}
