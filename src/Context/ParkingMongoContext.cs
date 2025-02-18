using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Login.Entity;

namespace Login.Infra.Context;

public class ParkingContext : DbContext
{
    public DbSet<Register>? Registers { get; set; }
    public ParkingContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var assembly = typeof(BaseEntity).Assembly;
        var entityTypes = assembly.GetTypes().Where(t => t.IsClass && typeof(BaseEntity).IsAssignableFrom(t));

        foreach (var entityType in entityTypes)
        {
            if (entityType.Name != typeof(BaseEntity).Name) modelBuilder.Entity(entityType).ToTable(entityType.Name);
        }



        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100);

            entity.Property(u => u.RA)
           .IsRequired()
           .HasMaxLength(100);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);
        });
    }
}