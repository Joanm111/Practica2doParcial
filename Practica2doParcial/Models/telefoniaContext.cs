using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica2doParcial.Models
{
    public partial class telefoniaContext : DbContext
    {
        public telefoniaContext()
        {
        }

        public telefoniaContext(DbContextOptions<telefoniaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Llamadas> Llamadas { get; set; }
        public virtual DbSet<Planes> Planes { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-448GVJA;Database=telefonia;User Id=joan;Password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Llamadas>(entity =>
            {
                entity.HasKey(e => e.CodLlamada);

                entity.Property(e => e.CodLlamada).HasColumnName("CodLLamada");

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TelefonoNavigation)
                    .WithMany(p => p.Llamadas)
                    .HasForeignKey(d => d.Telefono)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Llamadas_Telefono");
            });

            modelBuilder.Entity<Planes>(entity =>
            {
                entity.HasKey(e => e.IdPlan);

                entity.Property(e => e.IdPlan)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CostoMin).HasColumnType("money");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Renta).HasColumnType("money");
            });

            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Telefono1);

                entity.Property(e => e.Telefono1)
                    .HasColumnName("Telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPlan)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Telefono)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Telefono_Cliente");

                entity.HasOne(d => d.TipoPlanNavigation)
                    .WithMany(p => p.Telefono)
                    .HasForeignKey(d => d.TipoPlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Telefono_Planes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
