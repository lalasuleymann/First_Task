using System.Text.Json.Serialization;

namespace Task1_T.Models.Departments
{
    public class DepartmentGetAllResponse
    {
        [JsonPropertyName("departments")]
        public ICollection<DepartmentDto> DepartmentDtos { get; set; }

        public DepartmentGetAllResponse()
        {
            DepartmentDtos = new List<DepartmentDto>();
        }

    }
}
