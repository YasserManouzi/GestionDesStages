using GestionDesStagesTFYA.Shared.Models;

namespace GestionDesStagesTFYA.Server.Interfaces
{
    public interface IStageRepository
    {
        Stage AddStage(Stage stage);
        IEnumerable<Stage> GetAllStages();
        IEnumerable<Stage> GetAllStagesById(string id);

        Stage GetStageByStageId(string StageId);

        Stage UpdateStage(Stage stage);
    }
}
