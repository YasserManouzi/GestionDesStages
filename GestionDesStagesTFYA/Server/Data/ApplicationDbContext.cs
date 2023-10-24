using Duende.IdentityServer.EntityFramework.Options;
using GestionDesStagesTFYA.Server.Models;
using GestionDesStagesTFYA.Shared.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GestionDesStagesTFYA.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<StageStatut> StageStatut {get; set;}

        public DbSet<Stage> Stage { get; set; }

        public DbSet<Etudiant> Etudiant { get; set; }

        public DbSet<Entreprise> Entreprise { get; set; }
    }
  
}