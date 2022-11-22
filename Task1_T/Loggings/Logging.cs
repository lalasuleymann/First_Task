using Newtonsoft.Json;
using Task1_T.Models.Dtos.Loggings;

namespace Task1_T.Loggings
{
    public class Logging : ILogging
    {
        private readonly ILogger _logger;
        public Logging(ILogger<Logging> logger)
        {
            _logger = logger;
        }

        public async Task LogAction(string controllerName, string actionName, HttpContext context)
        {
            ActionLog actionLog = new()
            {
                ControllerName = controllerName,
                ActionName=actionName,
                Method= context.Request.Method,
                Path= context.Request.Path,
                IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                CreatedDate = DateTime.UtcNow
            };

            var log = JsonConvert.SerializeObject(actionLog);

            _logger.LogInformation(log);
        }

        public async Task LogError(Exception ex, HttpContext context)
        {
            var url = $"{context.Request.Scheme}://{context.Request.Host.ToUriComponent()}";

            ErrorLog error = new()
            {
                Exception = ex.StackTrace,
                ExceptionCode = $"{ex.Message}+ {DateTime.Now.ToString("ddMMyyyyhhmmss")}",
                Message = ex.Message,
                CreatedDate = DateTime.UtcNow,
                ErrorPage = url,
                IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? String.Empty
            };

            var log = JsonConvert.SerializeObject(error);
            _logger.LogError(log);
        }
    }
}
