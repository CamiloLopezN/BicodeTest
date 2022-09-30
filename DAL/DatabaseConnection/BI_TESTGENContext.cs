using System;
using System.Collections.Generic;
using BE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.DataBaseConnection
{
    public partial class BI_TESTGENContext : DbContext
    {
        public BI_TESTGENContext()
        {
        }

        public BI_TESTGENContext(DbContextOptions<BI_TESTGENContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documento> Documentos { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>(entity =>
            {
                entity.ToTable("Documento");

                entity.Property(e => e.Abreviatura).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.ToTable("Genero");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Apellido).HasMaxLength(200);

                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdDocumento)
                    .HasConstraintName("FK_Persona_Documento");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK_Persona_Genero");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
