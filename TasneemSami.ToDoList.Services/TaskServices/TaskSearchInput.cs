using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.GeniricRepository;

namespace TasneemSami.ToDoList.Services.TaskServices
{
    public class TaskSearchInput : PaginationInfo
    {
        public TaskSearchInput()
        {
            OrderBy = "PRIORITY";
        }
        public string? Title { get; set; }
        public int? UserId { get; set; }
        public bool? IsComplete { get; set; }
    }
}
