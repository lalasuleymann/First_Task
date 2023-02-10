using Task1_T.Models.Dtos;

namespace Task1_T.Models.Departments
{
    public class DepartmentDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
