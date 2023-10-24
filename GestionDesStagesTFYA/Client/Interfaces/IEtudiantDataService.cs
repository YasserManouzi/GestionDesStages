using GestionDesStagesTFYA.Shared.Models;

namespace GestionDesStagesTFYA.Client.Interfaces
{
    public interface IEtudiantDataService
    {
        Task<Etudiant> GetEtudiantById(string Id);
        Task<Etudiant> AddEtudiant(Etudiant etudiant);
        Task UpdateEtudiant(Etudiant etudiant);
    }
}
