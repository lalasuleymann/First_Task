using System.Text.Json.Serialization;

namespace Task1_T.Models.Dtos.Employees
{
    public class EmployeeGetAllResponse
    {
        [JsonPropertyName("employee")]
        public ICollection<EmployeeDto> EmployeeDtos { get; set; }

        public EmployeeGetAllResponse()
        {
            EmployeeDtos = new List<EmployeeDto>();
        }
    }
}
