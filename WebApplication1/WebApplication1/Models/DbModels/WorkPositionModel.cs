using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class WorkPositionModel
    {
        public int Id { get; set; }
        public string WorkPositionName { get; set; }
        public ICollection<EmployeeModel> Employee { get; set; }
        public bool IsActive { get; set; }
    }
}
