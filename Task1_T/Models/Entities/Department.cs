using System.ComponentModel.DataAnnotations.Schema;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class Department : CommonEntity
    {
        public string? Name { get; set; }
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
