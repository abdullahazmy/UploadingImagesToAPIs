using Day4API.Models;
using Day4API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Day4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitedController : ControllerBase
    {
        private readonly UnitOfWork _company;
        public UnitedController(UnitOfWork _comany)
        {
            this._company = _comany;
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            _company.Employee.Add(employee);
            _company.Department.Add(employee.Department);
            _company.Save();

            return Ok("Employee, Department Added Successfully");
        }
    }
}
