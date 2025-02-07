using System.ComponentModel.DataAnnotations.Schema;

namespace Day4API.Models
{
    public class Project_Department
    {
        //public int Id { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public int WorkingHours { get; set; }

        public virtual Project Project { get; set; }
        public virtual Department Department { get; set; }
    }
}
