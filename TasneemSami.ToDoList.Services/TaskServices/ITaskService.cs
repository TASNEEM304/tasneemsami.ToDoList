using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.GeniricRepository;
using TasneemSami.ToDoList.Database.Models;

namespace TasneemSami.ToDoList.Services.TaskServices
{
    public interface ITaskService : IRepository<Tasks>
    {
        GeneralOutput<TaskGetOutPutDto> GetAll(TaskSearchInput input);
        Task<TaskGetOutPutDto> GetById(int id);
        Task<TaskInsertAndUpdateDto> Insert(TaskInsertAndUpdateDto Object);
        Task<string> Edit(TaskInsertAndUpdateDto Object);
        Task<string> Delete(int id);
        Task<string> CompleteTask(int id);

    }
}
