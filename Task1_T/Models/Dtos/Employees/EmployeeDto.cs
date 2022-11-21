using System.Text.Json.Serialization;
using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.Employees
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int PositionId { get; set; }
    }
}
