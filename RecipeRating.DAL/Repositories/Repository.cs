using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using RecipeRating.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RecipeRating.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected readonly RecipeRatingDbContext _context;
        protected DbSet<T> _dbSet;

        public Repository(RecipeRatingDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Add(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            var result = _dbSet.Add(t).Entity;

            if (save)
            {
                SaveChanges();
            }

            return result;
        }

        public async Task<T> AddAsync(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            var result = (await _dbSet.AddAsync(t)).Entity;

            if (save)
            {
                await SaveChangesAsync();
            }

            return result;
        }

        public T AddOrUpdate(T t, bool save = false)
        {
            if (t != null) return t.Id == 0 ? Add(t, save) : Update(t, save);

            throw new ArgumentNullException();
        }

        public async Task<T> AddOrUpdateAsync(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            if (t.Id == 0)
            {
                return await AddAsync(t, save);
            }

            return await UpdateAsync(t, save);
        }

        public void Delete(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            Delete(t.Id, save);
        }

        public void Delete(int id, bool save = false)
        {
            var t = _dbSet.FirstOrDefault(i => i.Id == id);

            if (t != null)
            {
                _dbSet.Remove(t);
            }

            if (save)
            {
                SaveChanges();
            }
        }

        public void DeleteAll(List<int> ts, bool save = false)
        {
            foreach (var t in ts)
            {
                Delete(t);
            }

            if (save)
            {
                SaveChanges();
            }
        }

        public void DeleteAll(List<T> ts, bool save = false)
        {
            _dbSet.RemoveRange(ts);

            if (save)
            {
                SaveChanges();
            }
        }

        public async Task DeleteAsync(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            await DeleteAsync(t.Id, save);
        }

        public async Task DeleteAsync(int id, bool save = false)
        {
            var t = await _dbSet.FirstOrDefaultAsync(i => i.Id == id);

            if (t != null)
            {
                _dbSet.Remove(t);
            }

            if (save)
            {
                await SaveChangesAsync();
            }
        }

        public T Update(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            var existing = _context.Set<T>().Find(t.Id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(t);

                if (save)
                {
                    SaveChanges();
                }
            }
            else
            {
                // TODO: Add logging
            }

            return existing;
        }

        public async Task<T> UpdateAsync(T t, bool save = false)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }

            var existing = await _context.Set<T>().FindAsync(t.Id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(t);

                if (save)
                {
                    await SaveChangesAsync();
                }
            }
            else
            {
                // TODO: Add logging
            }

            return existing;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        private IQueryable<T> SetIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, [Optional] Expression<Func<T, bool>> expression)
        {
            var query = _dbSet.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            query = includes(query);
            return query.AsNoTracking().AsSplitQuery();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
