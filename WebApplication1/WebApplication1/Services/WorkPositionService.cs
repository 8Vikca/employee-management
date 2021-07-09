using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces.Repositories;
using WebApplication1.Interfaces.Services;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Services
{
    public class WorkPositionService: IWorkPositionService
    {

        private IWorkPositionRepository _workPositionRepository;

        public WorkPositionService(IWorkPositionRepository workPositionRepository)
        {
            _workPositionRepository = workPositionRepository;
        }

        public List<WorkPositionViewModel> GetAllActivePositions()
        {
            var listOfPositions = new List<WorkPositionViewModel>();

            var activePositions = _workPositionRepository.GetAllActiveWorkPositions().ToList();

            foreach (var activePosition in activePositions)
            {
                var workPositionViewModel = new WorkPositionViewModel
                {
                    WorkPositionName = activePosition.WorkPositionName
                };

                listOfPositions.Add(workPositionViewModel);
            }

            return listOfPositions;
        }
        public bool CreateNewWorkPosition(WorkPositionViewModel workPositionViewModel)
        {
            try
            {
                _workPositionRepository.CreateNewWorkPosition(workPositionViewModel);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
