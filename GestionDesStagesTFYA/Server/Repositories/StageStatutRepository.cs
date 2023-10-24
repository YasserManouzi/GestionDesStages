using GestionDesStagesTFYA.Server.Data;
using GestionDesStagesTFYA.Server.Interfaces;
using GestionDesStagesTFYA.Server.Models;

namespace GestionDesStagesTFYA.Server.Repositories
{
    public class StageStatutRepository : IStageStatutRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public StageStatutRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<StageStatut> GetAllStageStatuts()
        {
            return _appDbContext.StageStatut;
        }
    }
}
