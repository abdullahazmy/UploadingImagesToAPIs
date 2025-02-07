namespace Day4API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Project_Department> Project_Departments { get; set; } = new List<Project_Department>();

    }
}
