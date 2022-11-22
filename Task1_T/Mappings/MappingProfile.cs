using AutoMapper;
using Task1_T.Models.Departments;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Dtos.Positions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;

namespace Task1_T.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserPermission, PermissionDto>()
                .ForMember(x => x.Name, e => e.MapFrom(c => c.Permission.Name));
        }
    }
}
