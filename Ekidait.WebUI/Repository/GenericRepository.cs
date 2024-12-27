using Ekidait.Core.Entities;
using Ekidait.Data;
using Ekidait.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ekidait.Service.Concrete
{
    public class GenericRepository<T> : IService<T> where T : class, IEntity, new()
    {
        internal DatabaseContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
          await _dbSet.AddAsync(entity);
        }

        public Task AddAsync(int entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(AppUser appUser)
        {
            await _context.AppUsers.AddAsync(appUser);
            await _context.SaveChangesAsync();
        }

        public Task AddAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking().ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return  await _dbSet.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
