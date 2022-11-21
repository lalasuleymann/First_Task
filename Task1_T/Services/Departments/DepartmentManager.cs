using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Task1_T.Models.Departments;
using Task1_T.Models.Entities;
using Task1_T.Repositories;

namespace Task1_T.Services.Departments
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IBaseRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentManager(IBaseRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentGetResponse> GetDepartmentByIdAsync(int departmentId)
        {
            var response = new DepartmentGetResponse();
            var department = await _departmentRepository.GetFirstOrDefaultAsync(x=> x.Id==departmentId);
            response.DepartmentDto = _mapper.Map<DepartmentDto>(department);
            return response;
        }

        public async Task<DepartmentGetAllResponse> GetDepartmentsAsync()
        {
            var response = new DepartmentGetAllResponse();
            var entities = await _departmentRepository.GetAllAsync();
            response.DepartmentDtos = _mapper.Map<List<DepartmentDto>>(entities);
            return response;

        }

        public async Task<DepartmentGetResponse> CreateDepartmentAsync(SaveDepartmentRequest request)
        {
            DepartmentGetResponse response = new();

            Department department = new Department
            {
                Id = request.Id,
                Name = request.Name
            };

            var addedEntity = await _departmentRepository.AddAsync(department);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }

            var dto = _mapper.Map<DepartmentDto>(addedEntity);
            response.DepartmentDto = dto;
            return response;
        }

        public async Task UpdateDepartmentAsync(int departmentId, SaveDepartmentRequest request)
        {
            var item = await _departmentRepository.GetFirstOrDefaultAsync(x => x.Id == departmentId);

            item.Name = request.Name;

            await _departmentRepository.UpdateAsync(item);
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var entity = await _departmentRepository.GetFirstOrDefaultAsync(x => x.Id == departmentId);
            await _departmentRepository.DeleteAsync(entity);
        }
    }
}
