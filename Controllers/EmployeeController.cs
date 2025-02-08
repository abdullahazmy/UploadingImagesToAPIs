using Day4API.DTOs;
using Day4API.Models;
using Day4API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Day4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        UnitOfWork _repository;
        public EmployeeController(UnitOfWork _unitOfWork)
        {
            this._repository = _unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Eager Loading for Department
            List<Employee> e = _repository.Employee.SelectAll();
            List<EmployeeGetDataDTO> emp = new List<EmployeeGetDataDTO>();

            foreach (var item in e)
            {
                emp.Add(new EmployeeGetDataDTO
                {
                    Employee_ID = item.Id,
                    Employee_Name = item.Name,
                    EmployeeDepartmentName = item.Department.Name,
                    DepartmentDescription = item.Department.Description,
                    Department_ID = item.DepartmentId
                });
            }

            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Add(EmployeeGetDataDTO emp)
        {
            // Check if the department already exists
            var existingDepartment = _repository.Employee.SelectAll().FirstOrDefault(d => d.Department.Name == emp.EmployeeDepartmentName)?.Department;

            Employee e = new Employee
            {
                Name = emp.Employee_Name,
                DepartmentId = existingDepartment.Id, // Use existing department
                Department = existingDepartment
            };

            _repository.Employee.Add(e);

            string _Path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            FileStream _Stream = new FileStream(Path.Combine(_Path, e.Id + emp.Image.FileName), FileMode.Create);
            emp.Image.CopyTo(_Stream); // Save the image

            _repository.Save();
            return Ok(emp);
        }
    }
}
