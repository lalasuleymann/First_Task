using System.Text.Json.Serialization;

namespace Task1_T.Models.Departments
{
    public class DepartmentGetResponse
    {
        [JsonPropertyName("department")]
        public DepartmentDto DepartmentDto { get; set; }
    }
}
