using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class Employee : CommonEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

        public int EmployeeParentId { get; set; }
        [ForeignKey("EmployeeParentId")]
        public ICollection<Employee> Children { get; set; }
    }
}
