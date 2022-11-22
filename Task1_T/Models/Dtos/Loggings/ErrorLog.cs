namespace Task1_T.Models.Dtos.Loggings
{
    public class ErrorLog
    {
        public string ExceptionCode { get; set; }
        public string? Exception { get; set; }
        public string Message { get; set; }
        public string ErrorPage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}
