using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Database.GeniricRepository
{
    public class PaginationInfo
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string? search { get; set; }
        public string? OrderBy { get; set; }
        public bool Reverse { get; set; } = false;


    }
}
