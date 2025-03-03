﻿using System;
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
                    WorkPositionName = activePosition.WorkPositionName,
                    Id = activePosition.Id 
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

        public bool DeletePosition(WorkPositionViewModel workPositionViewModel)
        {
            try
            {
                var workPositionByName = _workPositionRepository.FindPositionByName(workPositionViewModel.WorkPositionName);
                if (workPositionByName == null)
                {
                    return false;
                }
                _workPositionRepository.RemoveWorkPosition(workPositionByName);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<HistoryOfWorkPositionsViewModel> GetHistoryOfWorkPositions(int employeeId)
        {
            var historyPositionsByEmployee = _workPositionRepository.GetHistoryOfPositionsByEmployee(employeeId).ToList();

             var listOfHistoryWorkPositions = new List<HistoryOfWorkPositionsViewModel>();


            foreach (var historyPosition in historyPositionsByEmployee)
            {
                var historyPositionViewModel = new HistoryOfWorkPositionsViewModel
                {
                    Id = historyPosition.Id,
                    WorkPositionName = historyPosition.WorkPosition.WorkPositionName,
                    StartDate = historyPosition.StartDate.ToString(),
                    EndDate = historyPosition.EndDate.ToString()
                };
                listOfHistoryWorkPositions.Add(historyPositionViewModel);
            }

            return listOfHistoryWorkPositions;
        }
    }
}
