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
        public virtual DbSet<Resultado> Resultados { get; set; } = null!;
        public virtual DbSet<Vhistorialresultado> Vhistorialresultados { get; set; } = null!;
        public virtual DbSet<Vpreguntasaleatoria> Vpreguntasaleatorias { get; set; } = null!;

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

                entity.Property(e => e.RespuestaCorrecta)
                    .HasMaxLength(1)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Resultado>(entity =>
            {
                entity.ToTable("resultados");

                entity.HasIndex(e => e.JugadorId, "JugadorID");

                entity.HasIndex(e => e.PreguntaId, "PreguntaID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaHoraRespuesta).HasColumnType("datetime");

                entity.Property(e => e.JugadorId).HasColumnName("JugadorID");

                entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");

                entity.Property(e => e.RespuestaElegida)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.Jugador)
                    .WithMany(p => p.Resultados)
                    .HasForeignKey(d => d.JugadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resultados_ibfk_1");

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.Resultados)
                    .HasForeignKey(d => d.PreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resultados_ibfk_2");
            });

            modelBuilder.Entity<Vhistorialresultado>(entity =>
            {
                entity.HasNoKey();
                //entity.Property(e => e.Jugador).HasColumnName("Jugador");

                entity.ToView("vhistorialresultados");

                entity.Property(e => e.Aciertos).HasPrecision(23);

                entity.Property(e => e.Jugador).HasMaxLength(50);
            });

            modelBuilder.Entity<Vpreguntasaleatoria>(entity =>
            {
                entity.HasNoKey();
                //entity.Property(e => e.Pregunta).HasColumnName("Pregunta");

                entity.ToView("vpreguntasaleatorias");

                entity.Property(e => e.OpcionA).HasMaxLength(200);

                entity.Property(e => e.OpcionB).HasMaxLength(200);

                entity.Property(e => e.OpcionC).HasMaxLength(200);

                entity.Property(e => e.Pregunta).HasMaxLength(500);

                entity.Property(e => e.RespuestaCorrecta)
                    .HasMaxLength(1)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
