using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.Data;

namespace TasneemSami.ToDoList.Database.GeniricRepository
{
    public abstract class Repository<Tcontext, T> : IRepository<T> where T : class where Tcontext : Data.ToDoListDbContext
    {
        protected Tcontext context;
        protected Repository(Tcontext Conext)
        {

            context = Conext;
        }
        public async Task<T> GetAsync<T>(int id) where T : class
        {
            return await context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetBy<T>(Expression<Func<T, bool>> Predict) where T : class
        {
            return context.Set<T>().AsNoTracking().Where(Predict);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> Insert<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Set<T>().Add(entity);
            await SaveChange();
            return entity;
        }
        public async Task<List<T>> InsertRange<T>(List<T> entites) where T : class
        {
            context.Set<T>().AddRange(entites);
            await SaveChange();
            return entites;
        }

        public async Task<T> Update<T>(T entity, int id) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            T oldentity = context.Set<T>().Find(id);
            context.Entry(oldentity).CurrentValues.SetValues(entity);
            await SaveChange();
            return entity;
        }

        public async Task<List<T>> UpdateRange<T>(List<T> entites, string key) where T : class
        {
            foreach (T entity in entites)
            {
                var id = context.Entry(entity).Property(key).CurrentValue;
                T oldentity = context.Set<T>().Find(id);
                context.Entry(oldentity).CurrentValues.SetValues(entity);
            }
            await SaveChange();
            return entites;
        }

        public async Task<bool> Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Set<T>().Remove(entity);
            await SaveChange();
            return true;
        }

        public async Task<bool> DeleteAllRange<T>(IQueryable<T> entity) where T : class
        {
            context.Set<T>().RemoveRange(entity);
            await SaveChange();
            return true;
        }



        public async Task SaveChange()
        {
            context.SaveChanges();
        }


    }
}
