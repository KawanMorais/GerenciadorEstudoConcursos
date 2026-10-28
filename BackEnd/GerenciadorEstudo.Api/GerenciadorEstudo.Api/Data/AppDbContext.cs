using GerenciadorEstudo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEstudo.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.FusoHorario)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(u => u.CriadoEm)
                .IsRequired();
        });
    }
}
