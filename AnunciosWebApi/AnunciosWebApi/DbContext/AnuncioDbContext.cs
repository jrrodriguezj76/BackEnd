using System;
using AnunciosWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnunciosWebApi.DBContext
{
    public partial class AnuncioDbContext : DbContext
    {
        public AnuncioDbContext()
        {
        }

        public AnuncioDbContext(DbContextOptions<AnuncioDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anuncio> Anuncios { get; set; }
        public virtual DbSet<Correo> Correos { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql("Host=saea98.com;port=5433;Database=ca_1;Username=ca_1;Password=4i5r25u8ca1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C.UTF-8");

            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Anuncios_Id_seq1\"'::regclass)");
               
                entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Correo>(entity =>
            {
                entity.Property(e => e.IdCorreo).HasDefaultValueSql("nextval('\"Correos_IdCorreo_seq\"'::regclass)");

                entity.HasKey(e => e.IdCorreo)
                    .HasName("Correos_pkey");

                //entity.Property(e => e.IdCorreo).ValueGeneratedNever();

                entity.Property(e => e.Correo1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Correo");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.Property(e => e.Id_Tipo).HasDefaultValueSql("nextval('\"Tipos_Id_Tipo_seq\"'::regclass)");

                entity.HasKey(e => e.Id_Tipo)
                    .HasName("Tipos_pkey");

                //entity.Property(e => e.IdTipo)
                //    .ValueGeneratedNever()
                //    .HasColumnName("Id_Tipo");

                entity.Property(e => e.Tipo1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Tipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
