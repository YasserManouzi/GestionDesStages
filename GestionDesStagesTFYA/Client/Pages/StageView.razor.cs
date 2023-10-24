using GestionDesStagesTFYA.Client.Interfaces;
using GestionDesStagesTFYA.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace GestionDesStagesTFYA.Client.Pages
{
    public partial class StageView
    {
        [Inject]
        public IStageDataService StageDataService { get; set; }

        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        public List<Stage> Stages { get; set; } = new List<Stage>();
        protected override async Task OnInitializedAsync()
        {
            if (await ObtenirClaim("role") == "Etudiant")
            {
                Stages = (await StageDataService.GetAllStages()).ToList();
            }
            else
            {
                Stages = (await StageDataService.GetAllStages(await ObtenirClaim("sub"))).ToList();
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
            // Obtenir du tableau des revendications le claim demandé pour l'utilisateur en cours
            // Attention s'il y a plusieurs claims du  même type (comme rôle) seul le premier est retourné FindFirst
            return user.FindFirst(c => c.Type == ClaimName)?.Value; ;
        }
    }



}

