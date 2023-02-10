using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Realms.Sync;
using System.Collections;
using System.Collections.Generic;
using Task1_T.Data;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Employees
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeManager(IUnitOfWorkService unitOfWork, IMapper mapper, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EmployeeGetResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var response = new EmployeeGetResponse();
            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == employeeId);
            response.EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return response;
        }

        public async Task<EmployeeGetAllResponse> GetEmployeesAsync()
        {
            var response = new EmployeeGetAllResponse();
            var employees = await _unitOfWork.EmployeeRepository.GetEmployees();
            response.EmployeeDtos = _mapper.Map<IList<EmployeeDto>>(employees);
            return response;
        }

        public async Task CreateEmployeeAsync(SaveEmployeeRequest request)
        {
            var position = await _unitOfWork.Positions.GetFirstOrDefaultAsync(x => x.Id == request.PositionId);
            if (position == null)
            {
                throw new Exception("Position does not exist in database!");
            }

            var employee = request.DepartmentIds.Select(departmentId => new Models.Entities.Employee
            {
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate,
                PositionId = request.PositionId,
                EmployeeParentId = request.EmployeeParentId != 0 ? request.EmployeeParentId : null,
            }).ToList();

            await _unitOfWork.Employees.AddMultipleAsync(employee);
            _unitOfWork.Complete();
        }

        public async Task UpdateEmployeeAsync(int employeeId, SaveEmployeeRequest request)
        {
            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x=> x.Id == employeeId);

            employee.Name = request.Name;
            employee.Surname = request.Surname;

            await _unitOfWork.Employees.UpdateAsync(employee);
            _unitOfWork.Complete(); 
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _unitOfWork.EmployeeRepository.DeleteEmployee(employeeId);
            _unitOfWork.Complete(); 
        }
    }
}