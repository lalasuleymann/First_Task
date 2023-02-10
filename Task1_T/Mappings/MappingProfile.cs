using AutoMapper;
using Task1_T.Models.Departments;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Dtos.Positions;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Models.Permissions;

namespace Task1_T.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(x => x.BirthDate, o => o.MapFrom(d => d.BirthDate.ToShortDateString()));
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<User, UserDto>();

            CreateMap<UserPermission, UserPermissionDto>()
                .ForMember(x => x.UserId, e => e.MapFrom(c => c.UserId)) 
                .ForMember(x => x.PermissionId, e => e.MapFrom(c => c.PermissionId));
        }
    }
}
