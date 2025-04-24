using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace TasneemSami.ToDoList.Database.GeniricRepository
{
    public class PagedList<T> where T : class
    {
        public static GeneralOutput<T> ToPagedList(IQueryable<T> q, PaginationInfo paginationInfo)
        {
            var query = paginationInfo.OrderBy != null ?
                q.OrderBy(paginationInfo.OrderBy + (paginationInfo.Reverse ? "desc" : "asc"))
                : q;

            int count = query.Count();
            query = query.Skip(paginationInfo.Skip).Take(paginationInfo.Take);

            return new GeneralOutput<T>
            {
                TotalSize = count,
                Items = query
            };
        }
    }
    public class GeneralOutput<T>
    {
        public int TotalSize { get; set; }
        public IQueryable<T> Items { get; set; }
    }
    public class GeneralOutputList<T>
    {

        public List<T> data { get; set; }
    }

}
