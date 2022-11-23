using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.Employees
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public Employee Employee { get; set; }
        public ICollection<Employee> Children { get; set; }
        public int? EmployeeParentId { get; set; }
        public int PositionId { get; set; }
    }
}
