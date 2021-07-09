using System;

namespace WebApplication1.Models.DbModels
{
    public class WorkPositionsHistoryModel
    {
        //realation 
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
        public int WorkPositionId { get; set; }
        public WorkPositionModel WorkPosition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
