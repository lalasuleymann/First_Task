using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using Task1_T.Data;
using Task1_T.Models.Dtos.EmployeeDepartment;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;
using Task1_T.Repositories.Employees;
using Task1_T.UnitOfWork;

namespace Task1_T.Repositories.Permissions
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeForParentId(int employeeId)
        {
            var parent = await
                (from e in _context.Employees
                 join eParent in _context.Employees
                 on e.EmployeeParentId equals eParent.Id
                 where e.Id == employeeId
                 select eParent).FirstOrDefaultAsync();

            return parent;    
        }


        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var children = await
                (from e in _context.Employees
                 join p in _context.Positions
                 on e.PositionId equals p.Id
                 select new EmployeeDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Surname = e.Surname,
                     BirthDate = e.BirthDate,
                     CreatedDate = e.CreatedDate,
                     ModifiedDate = e.ModifiedDate,
                     EmployeeParentId = e.EmployeeParentId,
                     EmployeeParentName = e.EmployeeParent.Name,
                     PositionName = p.Name,
                     PositionId = p.Id,
                     //EmployeeDepartments = e.EmployeeDepartments.ToList()
                 }).ToListAsync();
            return children;
        }


        public async Task<EmployeeDto?> GetFirst(int emp)
        {
            var children =
                (from ed in _context.EmployeeDepartments
                 join d in _context.Departments
                 on ed.DepartmentId equals d.Id
                 join e in _context.Employees
                 on ed.EmployeeId equals e.Id
                 join p in _context.Positions
                 on e.PositionId equals p.Id
                 select new EmployeeDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Surname = e.Surname,
                     BirthDate = e.BirthDate,
                     CreatedDate = e.CreatedDate,
                     ModifiedDate = e.ModifiedDate,
                     EmployeeParentId = e.EmployeeParentId,
                     EmployeeParentName = e.EmployeeParent.Name,
                     PositionName = p.Name,
                     PositionId = p.Id,
                 }).ToList().Where(x=> x.Id == emp).FirstOrDefault();
            return children;
        }


        public async Task<List<EmployeeDto>> GetDependentEmployees(int employeeId)
        {
            var dependents = await
                (from e in _context.Employees
                 join eParent in _context.Employees
                 on e.Children equals eParent.Children
                 where eParent.EmployeeParentId == employeeId
                 select new EmployeeDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Surname= e.Surname,
                     BirthDate= e.BirthDate,
                     PositionId= e.PositionId,
                     EmployeeParentId= e.EmployeeParentId,
                 }).ToListAsync();
            return dependents;  
            //sadece bir dependent employeeni getirir, hamsini?
        }


        public async Task DeleteEmployee(int employeeId)
        {
            var children = await
                (from ed in _context.EmployeeDepartments
                 join d in _context.Departments
                 on ed.DepartmentId equals d.Id
                 join e in _context.Employees
                 on ed.EmployeeId equals e.Id
                 join p in _context.Positions
                 on e.PositionId equals p.Id
                 where e.Id == employeeId
                 select e).ToListAsync();
            foreach (var child
                in children)
            {
                if (child != null)
                {
                    _context.Employees.Remove(child);
                }
            }
        }


        public Task UpdateEmployee(EmployeeDto emp)
        {
            var children =
                (from ed in _context.EmployeeDepartments
                 join d in _context.Departments
                 on ed.DepartmentId equals d.Id
                 join e in _context.Employees
                 on ed.EmployeeId equals e.Id
                 join p in _context.Positions
                 on e.PositionId equals p.Id
                 select new EmployeeDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Surname = e.Surname,
                     BirthDate = e.BirthDate,
                     CreatedDate = e.CreatedDate,
                     ModifiedDate = e.ModifiedDate,
                     EmployeeParentId = e.EmployeeParentId,
                     EmployeeParentName = e.EmployeeParent.Name,
                     PositionName = p.Name,
                     PositionId = p.Id,
                 }).ToList().Where(x => x.Id == emp.Id).FirstOrDefault();
            if (children != null)
            {
                _context.Update(children);
                children.ModifiedDate = DateTime.UtcNow;
            }
            return null;
        }

        public async Task<List<GetEmployeeDepartmentOutput>> GetDepartmentsByEmployeeId(int id)
        {
            var departments = await GetDepartmentsInternal(id: id);

            return departments;
        }


        private async Task<List<GetEmployeeDepartmentOutput>> GetDepartmentsInternal(int? id = null, string? email = null)
        {
            var departmentsQuery =
               from ed in _context.EmployeeDepartments
               join d in _context.Departments
          on ed.DepartmentId equals d.Id
               join e in _context.Employees
               on ed.EmployeeId equals e.Id
               select new { Department = d, EmployeeDepartment = ed, Employee = e };

            if (id.HasValue)
            {
                departmentsQuery = departmentsQuery.Where(p => p.Employee.Id == id);
            }

            var departments = await (from d in departmentsQuery
                                     select new GetEmployeeDepartmentOutput
                                     {
                                         Id = d.Department.Id,
                                         Name = d.Department.Name
                                     }).ToListAsync();

            return departments;
        }
    }
}
