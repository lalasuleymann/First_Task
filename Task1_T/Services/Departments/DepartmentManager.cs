using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Task1_T.Models.Departments;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Departments
{
    public class DeparmentManager : IDepartmentService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IMapper _mapper;
        public DeparmentManager(IUnitOfWorkService unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DepartmentGetResponse> GetDepartmentByIdAsync(int departmentId)
        {
            var response = new DepartmentGetResponse();
            var department = await _unitOfWork.Departments.GetFirstOrDefaultAsync(x=> x.Id==departmentId);
            response.DepartmentDto = _mapper.Map<DepartmentDto>(department);
            return response;
        }

        public async Task<DepartmentGetAllResponse> GetDepartmentsAsync()
        {
            var response = new DepartmentGetAllResponse();
            var entities = await _unitOfWork.Departments.GetAllAsync();
            response.DepartmentDtos = _mapper.Map<List<DepartmentDto>>(entities);
            return response;
        }

        public async Task<DepartmentGetResponse> CreateDepartmentAsync(SaveDepartmentRequest request)
        {
            DepartmentGetResponse response = new();

            Department department = new Department
            {
                Name = request.Name
            };

            var addedEntity = await _unitOfWork.Departments.AddAsync(department);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }
             _unitOfWork.Complete();

            var dto = _mapper.Map<DepartmentDto>(addedEntity);
            response.DepartmentDto = dto;
            return response;
        }

        public async Task UpdateDepartmentAsync(int departmentId, SaveDepartmentRequest request)
        {
            var item = await _unitOfWork.Departments.GetFirstOrDefaultAsync(x => x.Id == departmentId);
            if (item==null)
            {
                throw new Exception("Department does not exists!");
            }
            item.Name = request.Name;

            await _unitOfWork.Departments.UpdateAsync(item);
            _unitOfWork.Complete();
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var entity = await _unitOfWork.Departments.GetFirstOrDefaultAsync(x => x.Id == departmentId);
            if (entity == null)
            {
                throw new Exception("Department does not exists!");
            }
            await _unitOfWork.Departments.DeleteAsync(entity);
            _unitOfWork.Complete();
        }
    }
}
