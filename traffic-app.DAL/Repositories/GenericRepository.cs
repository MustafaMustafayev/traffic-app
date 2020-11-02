using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;

namespace traffic_app.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly TrafficDbContext _trafficDbContext;
        public GenericRepository(TrafficDbContext trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _trafficDbContext.AddAsync(entity);
            await _trafficDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> AddRange(List<TEntity> entity)
        {
            await _trafficDbContext.AddRangeAsync(entity);
            await _trafficDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> Delete(TEntity entity)
        {
             _trafficDbContext.Remove(entity);
            _trafficDbContext.Entry(entity).Property("CreatedAt").IsModified = false;
            _trafficDbContext.Entry(entity).Property("UpdatedAt").IsModified = false;
            return await _trafficDbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _trafficDbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? _trafficDbContext.Set<TEntity>().ToListAsync() : _trafficDbContext.Set<TEntity>().Where(filter).ToListAsync());
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _trafficDbContext.Update(entity);
            _trafficDbContext.Entry(entity).Property("CreatedAt").IsModified = false;
            await _trafficDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> UpdateRange(List<TEntity> entity)
        {
            _trafficDbContext.UpdateRange(entity);
            await _trafficDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
