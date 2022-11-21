using System.ComponentModel.DataAnnotations.Schema;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class Position : CommonEntity
    {   
        public string Name { get; set; }
        public Employee Employee { get; set; }

    }
}
