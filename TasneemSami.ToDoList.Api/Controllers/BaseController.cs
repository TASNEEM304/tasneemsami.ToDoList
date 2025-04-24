using Microsoft.AspNetCore.Mvc;
using System.IO.IsolatedStorage;
using TasneemSami.ToDoList.Api.Helper;
using TasneemSami.ToDoList.Services.ViewModel;

namespace TasneemSami.ToDoList.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(LogActionFilter))]
    public class BaseController : ControllerBase
    {
        private readonly IHosException _hosException;
        public BaseController(IHosException hosException)
        {
            _hosException = hosException;
        }
        [NonAction]
        public ResponseOutput Response(ResponseStatus status, object Data)
        {
            if (status == ResponseStatus.Error && Data is not string)

                Data = _hosException.Exception((Exception)Data);

            var json = new ResponseOutput
            {
                status = status,
                StatusDescription = status == ResponseStatus.Error ? Data + "" : ResponseStatus.Success.ToString(),
                Data = status != ResponseStatus.Error ? Data : null,
            };
            return json;


        }
    }
}
