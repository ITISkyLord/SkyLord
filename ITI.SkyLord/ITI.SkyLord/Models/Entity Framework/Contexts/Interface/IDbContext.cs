using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IDbContext
    {
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        IModel Model { get; }
        EntityEntry Add(object entity, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity, GraphBehavior behavior = GraphBehavior.IncludeDependents) where TEntity : class;
        void AddRange(params object[] entities);
        void AddRange(IEnumerable<object> entities, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        EntityEntry Attach(object entity, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity, GraphBehavior behavior = GraphBehavior.IncludeDependents) where TEntity : class;
        void AttachRange(params object[] entities);
        void AttachRange(IEnumerable<object> entities, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        void Dispose();
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);
        [DebuggerStepThrough]
        int SaveChanges();
        [DebuggerStepThrough]
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Update(object entity, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity, GraphBehavior behavior = GraphBehavior.IncludeDependents) where TEntity : class;
        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities, GraphBehavior behavior = GraphBehavior.IncludeDependents);
        void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}
