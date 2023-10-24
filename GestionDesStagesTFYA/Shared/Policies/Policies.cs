using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesStagesTFYA.Shared.Policies
{
    public static class Policies
    {
        public const string EstEntreprise = "EstEntreprise";
        public const string EstEtudiant = "EstEtudiant";
        public const string EstCoordonnateur = "EstCoordonnateur";


        public static AuthorizationPolicy EstEntreprisePolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireClaim("Statut", "Milieu")
                .RequireRole("Entreprise")
                .Build();
        }


        public static AuthorizationPolicy EstEtudiantPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireClaim("Statut", "Milieu")
                .RequireRole("Etudiant")
                .Build();
        }


        public static AuthorizationPolicy EstCoordonnateurPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireClaim("Statut", "Milieu")
                .RequireRole("Coordonnateur")
                .Build();
        }
    }
}
