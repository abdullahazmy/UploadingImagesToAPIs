using Day4API.DTOs;
using Day4API.Models;
using Day4API.Repository;
using Microsoft.AspNetCore.Mvc;


// NOTE: I have to handle the logic for the DepartmentController by Creating DTOs and Mapping them to the Department Model
namespace Day4API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //private readonly GenericRepository<Department> _repository;
        private readonly UnitOfWork _repository;
        public DepartmentController(UnitOfWork repository) =>
            _repository = repository;


        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _repository.Department.SelectAll();

            if (departments == null || !departments.Any())
                return NoContent(); // Returns HTTP 204 No Content

            List<DepartmentDTO> departmentDTOs = new List<DepartmentDTO>();
            foreach (var department in departments)
            {
                var departmentDTO = new DepartmentDTO
                {
                    DepId = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    Employees = department.Employees.Select(e => new EmployeeDepDTO
                    {
                        EmpId = e.Id,
                    }).ToList()
                    //Projects = department.Projects.Select(p => new ProjectDepDTO
                    //{
                    //    Id = p.Id,
                    //    ProjectName = p.ProjectName
                    //}).ToList()
                };
                departmentDTOs.Add(departmentDTO);
            }

            return Ok(departmentDTOs); // Returns HTTP 200 OK with data
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _repository.Department.SelectById(id);
            if (department == null)
                return NoContent(); // Returns HTTP 204 No Content

            DepartmentDTO departmentDTO = new DepartmentDTO
            {
                DepId = department.Id,
                Name = department.Name,
                Description = department.Description,
                Employees = department.Employees.Select(e => new EmployeeDepDTO
                {
                    EmpId = e.Id,
                }).ToList()
                //Projects = department.Projects.Select(p => new ProjectDepDTO
                //{
                //    Id = p.Id,
                //    ProjectName = p.ProjectName
                //}).ToList()
            };

            return Ok(departmentDTO); // Returns HTTP 200 OK with data
        }
        [HttpPost]
        public IActionResult AddDepartment([FromForm] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("Invalid department data.");

            var department = new Department
            {
                Id = departmentDto.DepId,  // Assuming ID is provided (or can be auto-generated)
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                Employees = departmentDto.Employees?.Select(e => new Employee
                {
                    Id = e.EmpId,  // Assuming ID is provided (or can be auto-generated)
                }).ToList() ?? new List<Employee>(),

                //Project_Departments = departmentDto.ProjectDepartments?.Select(p => new Project_Department
                //{
                //    ProjectId = p.ProjectId,
                //    ProjectName = p.ProjectName
                //}).ToList() ?? new List<Project_Department>()
            };

            _repository.Department.Add(department);
            _repository.Save();

            return CreatedAtAction(nameof(GetAll), new { id = department.Id }, department);
        }

        [HttpPut]
        public void UpdateDepartment([FromBody] Department department) => _repository.Department.Update(department);
        [HttpDelete("{id}")]
        public void DeleteDepartment(int id)
        {
            _repository.Department.Delete(id);
            _repository.Save();
        }

    }
}
