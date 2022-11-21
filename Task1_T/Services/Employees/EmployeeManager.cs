using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Entities;
using Task1_T.Repositories;

namespace Task1_T.Services.Employees
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeManager(IBaseRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeGetResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var response = new EmployeeGetResponse();
            var employee = await _employeeRepository.GetFirstOrDefaultAsync(x=>x.Id==employeeId);
            response.EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return response;
        }

        public async Task<EmployeeGetAllResponse> GetEmployeesAsync()
        {
            var response = new EmployeeGetAllResponse();
            var employees = await _employeeRepository.GetAllAsync();
            response.EmployeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return response;
        }

        public async Task<EmployeeGetResponse> CreateEmployeeAsync(SaveEmployeeRequest request)
        {
            var position = await _employeeRepository.GetFirstOrDefaultAsync(x => x.Id == request.PositionId);
            if (position==null)
            {
                throw new Exception("Position does not exist in database!");
            }

            EmployeeGetResponse response = new();

            //var department = request.Departments;
            //for (int i = 0; i < department.Length; i++)
            //{
            //    var items = new List<Department>(i);
            //}

            //var departments = request.DepartmentId;
            //for (int i = 0; i < departments.Length; i++) 
            //{
            //    var items = new List<Department>(i);
            //}

            Employee employee = new Employee
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate,
                PositionId = request.PositionId,
            };
            
            var addedEntity = await _employeeRepository.AddAsync(employee);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }

            var dto = _mapper.Map<EmployeeDto>(addedEntity);
            response.EmployeeDto = dto;
            return response;
        }

        public async Task UpdateEmployeeAsync(int employeeId, SaveEmployeeRequest request)
        {
            var employee = await _employeeRepository.GetFirstOrDefaultAsync(x => x.Id == employeeId);

            employee.Name = request.Name;
            employee.Surname = request.Surname;
            employee.BirthDate = request.BirthDate;
            employee.ModifiedDate = DateTime.UtcNow;

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetFirstOrDefaultAsync(x => x.Id == employeeId);

            await _employeeRepository.DeleteAsync(employee);
        }
    }
}
