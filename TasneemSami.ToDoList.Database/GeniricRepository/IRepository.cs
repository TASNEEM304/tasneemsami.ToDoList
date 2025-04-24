using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace TasneemSami.ToDoList.Database.GeniricRepository
{
    public interface IRepository<context>
    {
        Task<List<T>>GetAllAsync<T>() where T : class;
        IQueryable<T> GetBy<T>(Expression<Func<T,bool>> Predict) where T : class;
        Task<T> GetAsync<T>(int id) where T : class;
        Task<T> Insert<T>(T entity) where T : class;
        Task<List<T>> InsertRange<T>(List<T> entites) where T : class;
        Task<T> Update<T>(T entity,int id) where T : class;
        Task<List<T>> UpdateRange<T>(List<T> entites,string key) where T : class;

        Task<bool> Delete<T>(T entity) where T : class;
        Task<bool> DeleteAllRange<T>(IQueryable<T> entity) where T : class;
        Task SaveChange();

    }
}
