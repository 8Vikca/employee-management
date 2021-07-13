using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime OnBoardDate { get; set; }

        [Required]
        [ForeignKey("WorkPosition")]
        public int WorkPositionId { get; set; }
        
        public WorkPositionModel WorkPosition { get; set; }

        [Required]
        public double Salary { get; set; }

        public DateTime? ArchivedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
