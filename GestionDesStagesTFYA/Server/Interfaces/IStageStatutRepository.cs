using GestionDesStagesTFYA.Server.Models;

namespace GestionDesStagesTFYA.Server.Interfaces
{
    public interface IStageStatutRepository
    {
        IEnumerable<StageStatut> GetAllStageStatuts();
    }
}
