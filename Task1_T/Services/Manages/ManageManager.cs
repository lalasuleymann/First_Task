using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task1_T.Data;
using Task1_T.Models.Dtos.Employees;
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

        public async Task<ICollection<EmployeeDto>> GetDependentEmployeesAsync(int employeeId)
        {
            var employees = await _dbContext.Employees
                .Where(x => x.Id == employeeId)
                .Include(x => x.Children)
                .Select(s => new
                {
                    s.Name,
                    s.Surname,
                    s.BirthDate,
                    s.Position,
                    Children= s.Children.Select(s=>new
                    {
                        s.Name,
                        s.Surname,
                        s.BirthDate
                    })
                }).ToListAsync();
            var result = _mapper.Map<ICollection<EmployeeDto>>(employees);
            return result;
        }

        public async Task<ICollection<EmployeeDto>> GetManagerEmployeesAsync(int employeeId)
        {
            var employees = await _dbContext.Employees
                .Where(x => x.EmployeeParentId == employeeId)
                .Include(x => x.EmployeeParent)
                .Select(s=>new
                {
                    s.Name,
                    s.Surname,
                    s.BirthDate,
                    Parents= s.EmployeeParent
                }).ToListAsync();
            var result = _mapper.Map<ICollection<EmployeeDto>>(employees);
            return result;
        }
    }
}
