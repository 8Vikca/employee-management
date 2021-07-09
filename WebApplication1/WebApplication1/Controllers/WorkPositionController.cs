using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Interfaces.Services;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkPositionController : ControllerBase
    {
        private IWorkPositionService _workPositionService;

        public WorkPositionController(IWorkPositionService workPositionService)
        {
            _workPositionService = workPositionService;
        }

        [HttpGet("/getWorkPositions")]
        public List<WorkPositionViewModel> GetPositions()
        {
            return _workPositionService.GetAllActivePositions();
        }

        [HttpPost("/createWorkPosition")]
        public IActionResult CreatePosition([FromBody] WorkPositionViewModel newWorkPositionViewModel)
        {
            if (_workPositionService.CreateNewWorkPosition(newWorkPositionViewModel))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("/deleteWorkPosition")]
        public IActionResult DeletePosition()
        {
            return Ok();
        }
    }
}
