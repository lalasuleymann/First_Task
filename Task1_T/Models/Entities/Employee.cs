using System.ComponentModel.DataAnnotations;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class Employee : CommonEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public Position Position { get; set; }
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
