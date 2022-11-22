namespace Task1_T.Loggings
{
    public interface ILogging
    {
        Task LogAction(string controllerName, string actionName, HttpContext context);
        Task LogError(Exception ex, HttpContext context);
    }
}
