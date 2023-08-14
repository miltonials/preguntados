using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using preguntados_backend.Models;

namespace preguntados.Models
{
    /// <summary>
    /// Esta es una clase autogenerada por el Entity Framework que modela la base de datos
    /// </summary>
    /// 
    public partial class preguntadosContext : DbContext
    {
        public preguntadosContext()
        {
        }

        public preguntadosContext(DbContextOptions<preguntadosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jugadore> Jugadores { get; set; } = null!;
        public virtual DbSet<Historial> Historial { get; set; } = null!;
        public virtual DbSet<Pregunta> Preguntas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        /// <summary>
        /// Este método configura la base de datos para que use el charset utf8mb4
        /// e indica que las tablas deben usar el collation utf8mb4_0900_ai_ci.
        /// Además, configura la estructura de las tablas de la base de datos.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Jugadore>(entity =>
            {
                entity.ToTable("jugadores");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.ToTable("historial");

                entity.HasIndex(e => e.Id, "ID");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.JugadorId).HasColumnName("jugadorID");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vpreguntasaleatorias");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.OpcionA).HasMaxLength(200);

                entity.Property(e => e.OpcionB).HasMaxLength(200);

                entity.Property(e => e.OpcionC).HasMaxLength(200);

                entity.Property(e => e.Enunciado).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
