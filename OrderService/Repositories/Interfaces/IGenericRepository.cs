using System.Linq.Expressions;

namespace OrderService.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetById(object id);
    Task <T> Add(T entity); 
    Task<bool> AddRange(IEnumerable<T> entities);
    bool Update(T entity);
    bool UpdateRange(IEnumerable<T> entities);
    bool Delete(T entity);
    bool DeleteRange(IEnumerable<T> entities);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    
}