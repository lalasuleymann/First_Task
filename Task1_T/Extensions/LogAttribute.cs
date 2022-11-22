using Microsoft.AspNetCore.Mvc.Filters;
using Task1_T.Loggings;

namespace Task1_T.Extensions
{
    public class LogAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var controllerName = context.ActionDescriptor.RouteValues["Controller"].ToString();
            var actionName = context.ActionDescriptor.RouteValues["Action"].ToString();

            ILogging? logging = (ILogging)context.HttpContext.RequestServices.GetService(typeof(ILogging));


            logging.LogAction(controllerName, actionName, context.HttpContext);
            await next();
        }
    }
}
