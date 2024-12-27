using System.Linq.Expressions;
using TicketTrackingSystem.Core.Consts;

namespace TicketTrackingSystem.Core.Interface;
public interface IRepository<T> where T : class
{
    #region CRUD

    #region Create

    void Add(T entity);

    Task AddAsync(T entity);

    void AddRange(IEnumerable<T> entities);

    Task AddRangeAsync(IEnumerable<T> entities);

    #endregion Create

    #region Remove

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    #endregion Remove

    #region Update

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    #endregion Update
    #endregion
    T GetById(params object?[]? id);
    Task<T> GetByIdAsync(params object?[]? id);
    Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> match, bool tacking = true);
    Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> match, string[] includes = null, bool tacking = true);
    IQueryable<T> GetAllAsQueryable();
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(string[] includes = null);
    IEnumerable<T> GetAll(Expression<Func<T, bool>> match);
    IEnumerable<T> GetAll(Expression<Func<T, bool>> match, string[] includes = null);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(string[] includes = null);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null);
    Task<bool> CheckItemExistenceAsync(Expression<Func<T, bool>> match);
    Task<bool> CheckItemExistenceByIdAsync(params object?[]? key);
    Task<IEnumerable<T>> FindAndFilterAsync(string[] includes = null,
                                                                int? skip = null,
                                                                int? take = null,
                                                                Expression<Func<T, object>> orderBy = null,
                                                                string orderByDirection = OrderBy.Ascending,
                                                                bool tacking = true,
                                                                params Expression<Func<T, bool>>[] match);
    Task<IEnumerable<T>> FindAndFilterAsync(string[] includes = null,
                                                              int? skip = null,
                                                              int? take = null,
                                                              Expression<Func<T, object>> orderBy = null,
                                                              string orderByDirection = OrderBy.Ascending,
                                                              bool tacking = true);
}
