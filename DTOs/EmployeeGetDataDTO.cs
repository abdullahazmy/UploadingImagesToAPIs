namespace Day4API.DTOs
{
    public class EmployeeGetDataDTO
    {
        public int Employee_ID { get; set; }

        public int Department_ID { get; set; }
        public string Employee_Name { get; set; }

        public string EmployeeDepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public IFormFile Image { get; set; }
    }
}
