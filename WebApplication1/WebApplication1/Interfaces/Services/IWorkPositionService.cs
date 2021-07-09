using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Services
{
    public interface IWorkPositionService
    {
        // alphabetic order
        List<WorkPositionViewModel> GetAllActivePositions();
        bool CreateNewWorkPosition(WorkPositionViewModel workPositionViewModel);
        public bool DeletePosition(WorkPositionViewModel workPositionViewModel);
    }
}
