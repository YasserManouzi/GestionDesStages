using GestionDesStagesTFYA.Server.Interfaces;
using GestionDesStagesTFYA.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDesStagesTFYA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly IStageRepository _stageRepository;
        private readonly IStageStatutRepository stageStatutRepository;

        public StageController(IStageRepository stageRepository, IStageStatutRepository stageStatutRepository)
        {
            this._stageRepository = stageRepository;
            this.stageStatutRepository = stageStatutRepository;
        }

        [HttpPost]
        public IActionResult CreateStage([FromBody] Stage stage)
        {
            if (stage == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _stageRepository.AddStage(stage);

            return Created("stage", created);
        }
        [HttpGet]
        public IActionResult GetAllStage()
        {
            return Ok(_stageRepository.GetAllStages());
        }

        [HttpGet("{id}")]
        public IActionResult GetAllStage(string id)
        {
            return Ok(_stageRepository.GetAllStagesById(id));
        }

        [HttpGet("GetStageByStageId/{StageId}")]
        public IActionResult GetStageByStageId(string StageId)
        {
            return Ok(_stageRepository.GetStageByStageId(StageId));
        }

        [HttpPut]
        public IActionResult UpdateStage([FromBody] Stage stage)
        {
            if (stage == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // S'assurer que le stage existe dans la table avant de faire la mise à jour
            var stageToUpdate = _stageRepository.GetStageByStageId(stage.StageId.ToString());

            if (stageToUpdate == null)
                return NotFound();

            _stageRepository.UpdateStage(stage);

            return NoContent(); //success
        }
    }
}
