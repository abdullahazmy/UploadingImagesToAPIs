namespace Day4API.DTOs
{
    public class DepartmentDTO
    {
        public int DepId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<EmployeeDepDTO> Employees { get; set; } = new List<EmployeeDepDTO>();
        public List<ProjectDepDTO> Projects { get; set; } = new List<ProjectDepDTO>();
    }

    public class EmployeeDepDTO
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
    }
    public class ProjectDepDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }
}
