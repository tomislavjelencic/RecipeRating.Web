using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RecipeRating.Model.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T Add(T t, bool save = false);
        Task<T> AddAsync(T t, bool save = false);
        T AddOrUpdate(T t, bool save = false);
        Task<T> AddOrUpdateAsync(T t, bool save = false);
        void Delete(T t, bool save = false);
        Task DeleteAsync(T t, bool save = false);
        void Delete(int id, bool save = false);
        Task DeleteAsync(int id, bool save = false);
        void DeleteAll(List<int> ts, bool save = false);
        void DeleteAll(List<T> ts, bool save = false);
        IEnumerable<T> GetAll();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Update(T t, bool save = false);
        Task<T> UpdateAsync(T t, bool save = false);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
