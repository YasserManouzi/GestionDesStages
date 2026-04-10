using GestionDesStagesTFYA.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using GestionDesStagesTFYA.Client.Interfaces;
using GestionDesStagesTFYA.Server.Models;
using System.Security.Cryptography.X509Certificates;

namespace GestionDesStagesTFYA.Client.Pages
{
    public partial class StageEdit
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }

        [Inject]
        public IStageDataService StageDataService { get; set; }

        [Inject]
        public IStageStatutDataService StageStatutDataService { get; set; }

        [Parameter]
        public string StageId { get; set; }

        public Stage Stage { get; set; } = new Stage();

        public string LibelleBoutonEnregistrer { get; set; }
        
        public List<StageStatut> StageStatut { get; set; } = new List<StageStatut>();

        private bool enCoursDeSauvegarde = false;
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            // Appel du service pour obtenir la liste des status de stage
            StageStatut = (await StageStatutDataService.GetAllStageStatuts()).ToList();

            var result = Guid.TryParse(StageId, out var stageId);

            if (!result)
            {
                // On récupère intelligemment le premier ID disponible dans la base de données
                // (S'il n'y a aucun statut dans la base, on met 0 par sécurité)
                int defaultStatutId = StageStatut.Any() ? StageStatut.First().StageStatutId : 0;

                // Proposer des valeurs par défaut pour un nouveau stage
                Stage = new Stage
                {
                    StageStatutId = defaultStatutId, // Fini le '1' codé en dur !
                    Salaire = true,
                    DateCreation = DateTime.Now
                };
                LibelleBoutonEnregistrer = "Ajouter ce nouveau stage";
            }
            else
            {
                Stage = (await StageDataService.GetStageByStageId(StageId));
                LibelleBoutonEnregistrer = "Mettre à jour les informations du stage";
            }
        }



        protected async Task HandleValidSubmit()
        {
            enCoursDeSauvegarde = true;

            StateHasChanged();
            if (Stage.StageId == Guid.Empty) //new
            {
                // Obtenir du tableau des revendications (CLAIMS en anglais) le Id de l'utilisateur en cours
                Stage.Id = await ObtenirClaim("sub");
                // Obtenir un nouveau GUID pour le nouveau stage
                Stage.StageId = Guid.NewGuid();
                // Appel du service pour sauvegarder le nouveau stage dans la base de données.
                await StageDataService.AddStage(Stage);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                // Appel du service pour mettre à jour le stage existant dans la base de données.
                // Retourner à l'accueil
                await StageDataService.UpdateStage(Stage);
                NavigationManager.NavigateTo("/");
            }
        }

        protected void HandleInvalidSubmit()
        {
        }

        protected void NavigateToOverview()
        {
            if (Stage.StageId == Guid.Empty) //new
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                NavigationManager.NavigateTo("/stageview");
            }
        }


        /// <summary>
        /// Pour obtenir un claim : sid (Id de l'utilisateur actuel), sub, auth_time, idp, amr, role, preffered_username, name
        /// </summary>
        /// <param name="ClaimName"></param>
        /// <returns></returns>
        private async Task<string> ObtenirClaim(string ClaimName)
        {
            // Obtenir tous les revendications (Claims) de l'utilisateur actuellement connecté.            
            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User;
            IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
            // Mettre les revendications dans un tableau
            _claims = user.Claims;
            // Obtenir du tableau des revendications le Id de l'utilisateur en cours
            return user.FindFirst(c => c.Type == ClaimName)?.Value; ;
        }
    }
}
