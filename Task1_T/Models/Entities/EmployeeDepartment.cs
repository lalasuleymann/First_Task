using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class EmployeeDepartment : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }
}
