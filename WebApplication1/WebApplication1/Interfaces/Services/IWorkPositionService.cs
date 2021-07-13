using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Services
{
    public interface IWorkPositionService
    {
        bool CreateNewWorkPosition(WorkPositionViewModel workPositionViewModel);

        bool DeletePosition(WorkPositionViewModel workPositionViewModel);

        List<WorkPositionViewModel> GetAllActivePositions();

        List<HistoryOfWorkPositionsViewModel> GetHistoryOfWorkPositions(int employeeId);
    }
}
