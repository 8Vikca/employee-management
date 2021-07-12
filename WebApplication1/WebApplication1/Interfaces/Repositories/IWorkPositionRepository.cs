using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Repositories
{
    public interface IWorkPositionRepository
    {
        IEnumerable<WorkPositionModel> GetAllActiveWorkPositions();
        string GetWorkPositionById(int? id);
        bool CreateNewWorkPosition(WorkPositionViewModel newWorkPositionModel);
        WorkPositionModel FindPositionByName(string workPositionName);
        bool RemoveWorkPosition(WorkPositionModel workPosition);
    }
}
