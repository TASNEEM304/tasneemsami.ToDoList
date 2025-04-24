using Microsoft.AspNetCore.Mvc.Filters;
using TasneemSami.ToDoList.Api.Logger;

namespace TasneemSami.ToDoList.Api.Helper
{
    public class LogActionFilter:ActionFilterAttribute
    {
        private readonly ILoggerManger _logger;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public LogActionFilter(ILoggerManger logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _HttpContextAccessor = httpContextAccessor;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = _HttpContextAccessor?.HttpContext?.Request;
            string browser = request.Headers["sec-ch-ua"].ToString();
            string userHost = request.Host.Value;
            string method = request.Method;
            string? url = request.Path.Value;
            string requestparams = request.QueryString.Value;
            var userIdclaim=_HttpContextAccessor?.HttpContext?.User?.Claims?.Where(c=>c.Type=="Id").FirstOrDefault()?.Value;
            var userNameclaim = _HttpContextAccessor?.HttpContext?.User?.Claims?.Where(c => c.Type == "FirstName").FirstOrDefault()?.Value;
            _logger.LogDebug(string.Format("User Info:{0}/{1}-IP:{2}-Browser:{3}-Method:{4}-Url:{5}-Params:{6}",
                userIdclaim,
                userNameclaim,
                userHost,
                browser,
                method,
                url,
                requestparams
                
                
                ));



        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Result is Microsoft.AspNetCore.Mvc.ObjectResult objectresult)
            {
                var value = objectresult.Value;
                if (value != null)
                {
                    var statusProp = value.GetType().GetProperty("status");
                    var descriptionProp = value.GetType().GetProperty("StatusDescription");

                    if (statusProp != null && descriptionProp != null)
                    {
                        var status = statusProp.GetValue(value, null);
                        var desc = descriptionProp.GetValue(value, null);

                        _logger.LogDebug($"Info: Status: {status} - Value: {desc}");
                    }
                }
            }
        }

    }
}
