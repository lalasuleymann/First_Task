using System.Text.Json.Serialization;
using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.Employees
{
    public class SaveEmployeeRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public List<int> DepartmentIds { get; set; }
        public int PositionId { get; set; }
        public int EmployeeParentId { get; set; }
    }
}
