using Day4API.Models;

namespace Day4API.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly CompanyContext _context;
        public GenericRepository(CompanyContext context) =>
            _context = context;

        // Get All Employees
        public List<T> SelectAll() => _context.Set<T>().ToList();
        // Select Employee by ID
        public T SelectById(int id) => _context.Set<T>().Find(id);
        // Add Employee
        public void Add(T Entry) =>
            _context.Set<T>().Add(Entry);

        // Update Employee
        public void Update(T Entry) =>
            _context.Set<T>().Update(Entry);

        // Delete Employee
        public void Delete(int Id)
        {
            var ob = _context.Set<T>().Find(Id);
            _context.Set<T>().Remove(ob);
        }
        // Save Changes
        public void Save() => _context.SaveChanges();
    }
}
