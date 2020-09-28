using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        T Get(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        int Delete(T entity);
        List<T> AddRange(List<T> entity);
        List<T> UpdateRange(List<T> entity);
    }
}
