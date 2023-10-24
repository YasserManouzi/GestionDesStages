using GestionDesStagesTFYA.Server.Models;

namespace GestionDesStagesTFYA.Client.Interfaces
{
    public interface IStageStatutDataService
    {
        Task<IEnumerable<StageStatut>> GetAllStageStatuts();
    }
}
