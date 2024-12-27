using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketTrackingSystem.Core.Consts;
using TicketTrackingSystem.Core.Interface;

namespace TicketTrackingSystem.DAL.Implementation;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TicketTrackingSystemDbContext _context;
    public Repository(TicketTrackingSystemDbContext context)
    {
        _context = context;
    }

    #region CRUD
    #region Create
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }

    #endregion Create
    #region Remove
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    #endregion Remove
    #region Update
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }

    #endregion Update
    #endregion

    #region GetAll
    public IEnumerable<T> GetAll()
    {
        return FindQueryable(true).ToList();
    }

    public IEnumerable<T> GetAll(string[] includes = null)
    {
        IQueryable<T> query = FindQueryable(true);
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.ToList();

    }
    public IEnumerable<T> GetAll(Expression<Func<T, bool>> match)
    {
        IQueryable<T> query = FindQueryable(true);
        return query.Where(match).ToList();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> match, string[] includes = null)
    {
        IQueryable<T> query = FindQueryable(true);
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.Where(match).ToList();
    }

    #endregion GetAll
    #region GetAllAsync

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await FindQueryable(true).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
    {
        IQueryable<T> query = FindQueryable(true);
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
    {
        IQueryable<T> query = FindQueryable(true);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.Where(match).ToListAsync();
    }

    #endregion GetAllAsync

    public T GetById(params object?[]? id)
    {
        return _context.Set<T>().Find(id);
    }

    public IQueryable<T> GetAllAsQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }
    public async Task<bool> CheckItemExistenceByIdAsync(params object?[]? key)
    {
        return await _context.Set<T>().FindAsync(key) is not null;
    }

    public async Task<bool> CheckItemExistenceAsync(Expression<Func<T, bool>> match)
    {
        return await _context.Set<T>().AsNoTracking().AnyAsync(match);
    }

    public async Task<IEnumerable<T>> FindAndFilterAsync(string[] includes = null, int? skip = null, int? take = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC", bool tracking = true, params Expression<Func<T, bool>>[] match)
    {
        IQueryable<T> query = FindQueryable(tracking);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (match != null)
        {
            foreach (var item in match)
            {
                query = query.Where(item);
            }
        }
        if (orderBy != null)
        {
            if (orderByDirection == OrderBy.Ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAndFilterAsync(string[] includes = null, int? skip = null, int? take = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC", bool tracking = true)
    {
        IQueryable<T> query = FindQueryable(tracking);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (orderBy != null)
        {
            if (orderByDirection == OrderBy.Ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }
        return await query.ToListAsync();
    }
    private IQueryable<T> FindQueryable(bool tracking)
    {
        var queryable = GetAllAsQueryable();
        if (!tracking)
        {
            queryable = queryable.AsNoTracking();
        }
        return queryable;
    }

    public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> match, bool tacking = true)
    {
        IQueryable<T> query = FindQueryable(tacking);
        return await query.SingleOrDefaultAsync(match);
    }

    public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> match, string[] includes = null, bool tacking = true)
    {
        IQueryable<T> query = FindQueryable(tacking);
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.SingleOrDefaultAsync(match);
    }
    public async Task<T> GetByIdAsync(params object?[]? id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

}
