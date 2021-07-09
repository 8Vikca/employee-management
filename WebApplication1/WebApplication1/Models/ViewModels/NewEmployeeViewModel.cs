using System;

namespace WebApplication1.Models.ViewModels
{
    public class NewEmployeeViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }
        public string WorkPositionName { get; set; }
        public string Salary { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime OnBoardDate { get; set; }
    }
}
