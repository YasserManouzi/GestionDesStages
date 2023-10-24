using GestionDesStagesTFYA.Shared.Models;
namespace GestionDesStagesTFYA.Server.Interfaces
  
{
    public interface IEtudiantRepository
    {

        Etudiant GetEtudiantById(string Id);
        Etudiant AddEtudiant(Etudiant etudiant);
        Etudiant UpdateEtudiant(Etudiant etudiant);
    }
}
