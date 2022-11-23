using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Employees
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeManager(IUnitOfWorkService unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeGetResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var response = new EmployeeGetResponse();
            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x=>x.Id==employeeId);
            response.EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return response;
        }

        public async Task<EmployeeGetAllResponse> GetEmployeesAsync()
        {
            var response = new EmployeeGetAllResponse();
            var employees = await _unitOfWork.Employees.GetAllAsync();
            response.EmployeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return response;
        }

        public async Task<EmployeeGetResponse> CreateEmployeeAsync(SaveEmployeeRequest request)
        {
            var position = await _unitOfWork.Positions.GetFirstOrDefaultAsync(x => x.Id == request.PositionId);
            if (position==null)
            {
                throw new Exception("Position does not exist in database!");
            }

            EmployeeGetResponse response = new();

            Employee employee = new Employee
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate,
                PositionId = request.PositionId,
                //EmployeeDepartments= request.DepartmentId.ToArray(out employee)
            };
            
            var addedEntity = await _unitOfWork.Employees.AddAsync(employee);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }
            _unitOfWork.Complete();
            var dto = _mapper.Map<EmployeeDto>(addedEntity);
            response.EmployeeDto = dto;
            return response;
        }

        public async Task UpdateEmployeeAsync(int employeeId, SaveEmployeeRequest request)
        {
            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == employeeId);

            employee.Name = request.Name;
            employee.Surname = request.Surname;
            employee.BirthDate = request.BirthDate;
            employee.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.Employees.UpdateAsync(employee);
            _unitOfWork.Complete();
        }

        [ClaimRequirementFilter(PermissionNames.Employee.Delete)]
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee.EmployeeDepartments.Select(x => x.Employee) == null)
            {
                await _unitOfWork.Employees.DeleteAsync(employee);
            }
            _unitOfWork.Complete();
        }

    }
}
