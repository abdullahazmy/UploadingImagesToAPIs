using System.ComponentModel.DataAnnotations.Schema;

namespace Day4API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        // Foreign Key for Department => Note we didn't use it with Class Name but with Property Name
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        //[JsonIgnore]
        public virtual Department Department { get; set; }

    }
}
