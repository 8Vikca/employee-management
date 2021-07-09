using System;

namespace WebApplication1.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }
        public string WorkPositionName { get; set; }
        public double Salary { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
