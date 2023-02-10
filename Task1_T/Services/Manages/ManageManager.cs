using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task1_T.Data;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Manages
{
    public class ManageManager : IManagerService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ManageManager(IUnitOfWorkService unitOfWork, AppDbContext dbContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeGetResponse> GetManagerEmployeeAsync(int employeeId)
        {   
            var response = new EmployeeGetResponse();
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeForParentId(employeeId);
            response.EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return response;
        }
        public async Task<EmployeeGetAllResponse> GetDependentEmployeesAsync(int employeeId)
        {
            var response = new EmployeeGetAllResponse();
            //var employees = await _dbContext.Employees.FromSqlRaw($"[SelectEmployeeChildren] {employeeId}").ToListAsync();
            var employees = await _unitOfWork.EmployeeRepository.GetDependentEmployees(employeeId);
            response.EmployeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return response;
        }
    }
}