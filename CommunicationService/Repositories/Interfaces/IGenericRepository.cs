﻿using System.Linq.Expressions;

namespace CommunicationService.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> GetById(object id);
    Task <T> Add(T entity); 
    Task<bool> AddRange(IEnumerable<T> entities);
    bool Update(T entity);
    bool Delete(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
}