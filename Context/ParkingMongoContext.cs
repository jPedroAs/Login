using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.EntityFrameworkCore.Extensions;
using Parking.Entity;

namespace Parking.Infra.Context;

public class ParkingMongoContext : DbContext
{
    public DbSet<Register>? Registers { get; set; }
    public ParkingMongoContext(DbContextOptions options)
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
            if(entityType.Name != typeof(BaseEntity).Name) modelBuilder.Entity(entityType).ToCollection(entityType.Name);
        }
    }
}