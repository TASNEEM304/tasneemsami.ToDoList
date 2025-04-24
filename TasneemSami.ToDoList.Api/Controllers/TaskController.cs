using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasneemSami.ToDoList.Api.Helper;
using TasneemSami.ToDoList.Services.TaskServices;
using TasneemSami.ToDoList.Services.ViewModel;

namespace TasneemSami.ToDoList.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TaskController : BaseController
    {
        private readonly ITaskService _TaskService;
        private readonly IHosException _HosException;
        public TaskController(ITaskService TaskService, IHosException HosException):base(HosException)
        {
            _TaskService = TaskService;
            _HosException = HosException;
        }
        [HttpPost]
        [AllowAnonymous]
        public  ResponseOutput GetAll(TaskSearchInput input)
        {
            try
            {
                var res =  _TaskService.GetAll(input);
                return Response(ResponseStatus.Success, res);
            }
            catch(Exception ex)
            {
                return Response(ResponseStatus.Error, ex);
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<TaskGetOutPutDto> GetDetailById(int id)
        {
              var res = await _TaskService.GetById(id);
            return res;
           
        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ResponseOutput> Insert(TaskInsertAndUpdateDto input)
        {
            try
            {
                var res =  await _TaskService.Insert(input);
                return Response(ResponseStatus.Success, res);
            }
            catch (Exception ex)
            {
                return Response(ResponseStatus.Error, ex);
            }

        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ResponseOutput> Edit(TaskInsertAndUpdateDto input)
        {
            try
            {
                var res = await _TaskService.Edit(input);
                return Response(ResponseStatus.Success, res);
            }
            catch (Exception ex)
            {
                return Response(ResponseStatus.Error, ex);
            }

        }
        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ResponseOutput> Delete(int id)
        {
            try
            {
                var res = await _TaskService.Delete(id);
                return Response(ResponseStatus.Success, res);
            }
            catch (Exception ex)
            {
                return Response(ResponseStatus.Error, ex);
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<string> IsComplete(int id)
        {
            var res = await _TaskService.CompleteTask(id);
            return res;

        }
    }
}
