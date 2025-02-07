using Day4API.DTOs;
using Day4API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        CompanyContext _context;
        public EmployeeController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Eager Loading for Department
            List<Employee> e = _context.Employees.Include(emp => emp.Department).ToList();
            List<EmployeeGetDataDTO> emp = new List<EmployeeGetDataDTO>();

            foreach (var item in e)
            {
                emp.Add(new EmployeeGetDataDTO
                {
                    Employee_ID = item.Id,
                    Employee_Name = item.Name,
                    EmployeeDepartmentName = item.Department.Name
                });
            }
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Add(EmployeeGetDataDTO emp)
        {
            Employee e = new Employee
            {
                Name = emp.Employee_Name,
                DepartmentId = emp.Department_ID,
                Department = new Department { Name = emp.EmployeeDepartmentName, Description = emp.DepartmentDescription }
            };

            _context.Employees.Add(e);
            _context.SaveChanges();

            //return CreatedAtAction(nameof(GetAll), new { });
            return Ok(emp);
        }
    }
}
