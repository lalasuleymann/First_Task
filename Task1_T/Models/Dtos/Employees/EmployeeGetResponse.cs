using System.Text.Json.Serialization;

namespace Task1_T.Models.Dtos.Employees
{
    public class EmployeeGetResponse
    {
        [JsonPropertyName("employee")]
        public EmployeeDto EmployeeDto { get; set; }
    }
}
