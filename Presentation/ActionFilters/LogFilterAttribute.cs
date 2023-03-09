using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;


namespace Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
        }
        private string Log(string modelName, RouteData route)
        {
            var logDetails = new LogDetails()
            {
                ModelModel =modelName,
                Controller = route.Values["controller"],
                Action = route.Values["action"],

            };
            if(route.Values.Count>= 3) 
            {
                logDetails.Id = route.Values["Id"];
            }
            return logDetails.ToString();
        }
    }
}
