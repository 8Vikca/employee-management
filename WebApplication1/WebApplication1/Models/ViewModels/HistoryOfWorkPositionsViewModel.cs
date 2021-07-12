using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class HistoryOfWorkPositionsViewModel
    {
        public int Id { get; set; }
        public string WorkPositionName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
