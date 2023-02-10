using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class EmployeeDepartment : CommonEntity
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }

        public Employee Employee { get; set; }
        public Department Department { get; set; }
    }
}
