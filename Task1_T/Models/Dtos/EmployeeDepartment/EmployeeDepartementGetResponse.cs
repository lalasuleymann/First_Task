using System.Text.Json.Serialization;
using Task1_T.Models.Dtos.UserPermissions;

namespace Task1_T.Models.Dtos.EmployeeDepartment
{
    public class EmployeeDepartementGetResponse : BaseDto
    {
        [JsonPropertyName("employeeDepartment")]
        public EmployeeDepartmentDto EmployeeDepartmentDto { get; set; }
    }
}
