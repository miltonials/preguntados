using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace preguntados.Models
{
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
        public virtual DbSet<Pregunta> Preguntas { get; set; } = null!;
        public virtual DbSet<Historial> Historial { get; set; } = null!;
        public virtual DbSet<Vpreguntasaleatorias> Vpreguntasaleatorias { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

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

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.ToTable("preguntas");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OpcionA).HasMaxLength(200);

                entity.Property(e => e.OpcionB).HasMaxLength(200);

                entity.Property(e => e.OpcionC).HasMaxLength(200);

                entity.Property(e => e.Pregunta1)
                    .HasMaxLength(500)
                    .HasColumnName("Pregunta");

                //entity.Property(e => e.RespuestaCorrecta)
                //    .HasMaxLength(1)
                //    .IsFixedLength();
            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.ToTable("historial");

                entity.HasIndex(e => e.Id, "ID");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.JugadorId).HasColumnName("jugadorID");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.HasOne(d => d.Jugador)
                    .WithMany(p => p.Resultados)
                    .HasForeignKey(d => d.JugadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historial_ibfk_1");
            });

            modelBuilder.Entity<Vpreguntasaleatorias>(entity =>
            {
                entity.HasNoKey();
                //entity.Property(e => e.Pregunta).HasColumnName("Pregunta");

                entity.ToView("vpreguntasaleatorias");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.OpcionA).HasMaxLength(200);

                entity.Property(e => e.OpcionB).HasMaxLength(200);

                entity.Property(e => e.OpcionC).HasMaxLength(200);

                entity.Property(e => e.Pregunta).HasMaxLength(500);

                //entity.Property(e => e.RespuestaCorrecta)
                //    .HasMaxLength(1)
                //    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
