using Data.Identity;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AutorEntity> Autores { get; set; }
        public DbSet<LibroEntity> Libros { get; set; }
        public DbSet<PrestamoEntity> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorEntity>(entity =>
            {
                entity.ToTable("Autores");

                entity.HasKey(e => e.autor_id);

                entity.Property(e => e.autor_id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.nacionalidad)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property<DateTime>("CreatedAt")
                   .IsRequired();

                entity.Property<DateTime>("UpdatedAt");

            });

            modelBuilder.Entity<LibroEntity>(entity =>
            {
                entity.ToTable("Libros");

                entity.HasKey(e => e.libro_id);

                entity.Property(e => e.libro_id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.titulo)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.autor_id)
                    .IsRequired();

                entity.Property(e => e.ano_publicacion)
                    .IsRequired();

                entity.Property(e => e.genero)
                   .IsRequired()
                   .HasMaxLength(15);

                entity.HasOne(e => e.autor)
                    .WithMany()
                    .HasForeignKey(e => e.autor_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.autor_id);
                entity.HasIndex(e => e.genero);
                entity.HasIndex(e => e.ano_publicacion);

                entity.Property<DateTime>("CreatedAt")
                   .IsRequired();

                entity.Property<DateTime>("UpdatedAt");

            });

            modelBuilder.Entity<PrestamoEntity>(entity =>
            {

                entity.ToTable("Prestamos");

                entity.HasKey(e => e.prestamo_id);

                entity.Property(e => e.prestamo_id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.libro_id)
                   .IsRequired();

                entity.Property(e => e.fecha_prestamo)
                    .IsRequired();

                entity.Property(e => e.fecha_devolucion);
                    

                entity.HasOne(e => e.libro)
                    .WithMany()
                    .HasForeignKey(e => e.libro_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.libro_id);
                entity.HasIndex(e => e.fecha_prestamo);
                entity.HasIndex(e => e.fecha_devolucion);

                entity.Property<DateTime>("CreatedAt")
                   .IsRequired();

                entity.Property<DateTime>("UpdatedAt");
                   

            });

            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }



        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Metadata.FindProperty("CreatedAt") != null)
                    {
                        entry.Property("CreatedAt")
                            .CurrentValue = DateTime.UtcNow.AddHours(-4);
                    }
                }


                if (entry.State == EntityState.Modified)
                {
                    if (entry.Metadata.FindProperty("UpdatedAt") != null)
                    {
                        entry.Property("UpdatedAt")
                            .CurrentValue = DateTime.UtcNow.AddHours(-4);
                    }
                }
            }
        }

    }
}
