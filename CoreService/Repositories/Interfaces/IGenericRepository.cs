using System.Linq.Expressions;

namespace CoreService.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetById(object id);
    Task <T> Add(T entity); 
    Task<bool> AddRange(IEnumerable<T> entities);
    bool Update(T entity);
    bool Delete(T entity);
    bool DeleteRange(List<T> entity);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    
}
