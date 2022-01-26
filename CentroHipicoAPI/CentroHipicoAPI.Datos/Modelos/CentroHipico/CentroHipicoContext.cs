using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class CentroHipicoContext : DbContext
    {
        public CentroHipicoContext()
        {
        }

        public CentroHipicoContext(DbContextOptions<CentroHipicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<CarreraDetalle> CarreraDetalles { get; set; }
        public virtual DbSet<CarreraEjemplar> CarreraEjemplares { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Ejemplar> Ejemplares { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCarrera).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.MontoGanancia).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.MontoSubTotal).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.MontoTotal).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarreraDetalle>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.MontoApuesta).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.IdCarreraNavigation)
                    .WithMany(p => p.CarreraDetalles)
                    .HasForeignKey(d => d.IdCarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarreraDe__IdCar__440B1D61");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.CarreraDetalles)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarreraDe__IdCli__44FF419A");

                entity.HasOne(d => d.IdEjemplarNavigation)
                    .WithMany(p => p.CarreraDetalles)
                    .HasForeignKey(d => d.IdEjemplar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarreraDe__IdEje__45F365D3");
            });

            modelBuilder.Entity<CarreraEjemplar>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdCarreraNavigation)
                    .WithMany(p => p.CarreraEjemplares)
                    .HasForeignKey(d => d.IdCarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarreraEj__IdCar__412EB0B6");

                entity.HasOne(d => d.IdEjemplarNavigation)
                    .WithMany(p => p.CarreraEjemplares)
                    .HasForeignKey(d => d.IdEjemplar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarreraEj__IdEje__403A8C7D");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ejemplar>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E40BFEA1C4")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
