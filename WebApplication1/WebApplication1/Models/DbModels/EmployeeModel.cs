using System;
using System.ComponentModel.DataAnnotations;

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
        public int WorkPositionId { get; set; }
        
        [Required]
        public WorkPositionModel WorkPosition { get; set; }

        [Required]
        public double Salary { get; set; }

        public DateTime? DeletedDate { get; set; }

    }
}
