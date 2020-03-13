using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pmo.Controllers
{
    public class BaseStageComponentController : BaseProjectDetailController {
        protected readonly int _stageNumber;
        protected readonly int _stageId;
        protected readonly int _stageConfigId;
        protected readonly IActionResult _editAction;

        public BaseStageComponentController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            // set nav component for all of these pages here
            this._stageNumber = int.Parse(ViewModels.Helpers.Helpers.GetRouteValue(httpContextAccessor.HttpContext.Request, "stageNumber"));
            this._stageConfigId = _context.StageConfigs.First(f => f.StageNumber == _stageNumber).Id;
            this._stageId = _context.Stages.First(w => w.StageNumber == _stageNumber && w.ProjectId == _projectId).Id;
            this._editAction = RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
        }
    }
}