namespace Task1_T.Models.Dtos.Loggings
{
    public class ActionLog
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}
