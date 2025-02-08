using Day4API.Models;

namespace Day4API.Repository
{
    public class UnitOfWork // Like Singleton Pattern but for Database
    {
        CompanyContext _context;

        GenericRepository<Employee> EmployeeRepository;
        GenericRepository<Department> DepartmentRepository;

        public UnitOfWork(CompanyContext context) => _context = context;

        public GenericRepository<Employee> Employee
        {
            get
            {
                if (EmployeeRepository == null)
                {
                    EmployeeRepository = new GenericRepository<Employee>(_context);
                }
                return EmployeeRepository;
            }
        }

        public GenericRepository<Department> Department
        {
            get
            {
                if (DepartmentRepository == null)
                {
                    DepartmentRepository = new GenericRepository<Department>(_context);
                }
                return DepartmentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
