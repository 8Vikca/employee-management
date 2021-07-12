using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DbModels
{
    public class WorkPositionsHistoryModel
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        public EmployeeModel Employee { get; set; }

        [ForeignKey("WorkPosition")]
        public int? WorkPositionId { get; set; }

        public WorkPositionModel WorkPosition { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
