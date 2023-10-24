using GestionDesStagesTFYA.Shared.Models;

namespace GestionDesStagesTFYA.Client.Interfaces
{
    public interface IEntrepriseDataService
    {

        Task<Entreprise> GetEntrepriseById(string Id);
        Task<Entreprise> AddEntreprise(Entreprise entreprise);
        Task UpdateEntreprise(Entreprise entreprise);
    }
}
