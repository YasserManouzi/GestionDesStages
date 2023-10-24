using GestionDesStagesTFYA.Shared.Models;
namespace GestionDesStagesTFYA.Server.Interfaces

{
    public interface IEntrepriseRepository
    {

        Entreprise GetEntrepriseById(string Id);
        Entreprise AddEntreprise(Entreprise entreprise);
        Entreprise UpdateEntreprise(Entreprise entreprise);
    }
}
