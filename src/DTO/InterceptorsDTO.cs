using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Login.Entity;

public class InterceptorsDTO : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> interceptor,
        CancellationToken cancellationToken = default)
    {
        var et = eventData.Context;
        if(et == null)  return await base.SavingChangesAsync(eventData,interceptor,cancellationToken);
        
        var entities = et.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity);

        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                if (eventData.Context.Entry(entity).State == EntityState.Added)
                {
                    baseEntity.CreateAt = DateTime.UtcNow;
                    baseEntity.Actived = true;
                }
            }
        }

        return await base.SavingChangesAsync(eventData,interceptor,cancellationToken) ;
    }
}