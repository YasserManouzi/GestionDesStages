using GestionDesStagesTFYA.Client;
using GestionDesStagesTFYA.Client.Interfaces;
using GestionDesStagesTFYA.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("GestionDesStagesTFYA.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GestionDesStagesTFYA.ServerAPI"));

builder.Services.AddApiAuthorization();

// Pour appliquer les policy
builder.Services.AddAuthorizationCore(authorizationOptions =>
{
    authorizationOptions.AddPolicy(
        GestionDesStagesTFYA.Shared.Policies.Policies.EstEntreprise,
        GestionDesStagesTFYA.Shared.Policies.Policies.EstEntreprisePolicy());
    authorizationOptions.AddPolicy(
        GestionDesStagesTFYA.Shared.Policies.Policies.EstEtudiant,
        GestionDesStagesTFYA.Shared.Policies.Policies.EstEtudiantPolicy());
    authorizationOptions.AddPolicy(
        GestionDesStagesTFYA.Shared.Policies.Policies.EstCoordonnateur,
        GestionDesStagesTFYA.Shared.Policies.Policies.EstCoordonnateurPolicy());
});

//TODO: Modifier les points de terminations pour la production
builder.Services.AddScoped<IStageDataService, StageDataService>();
builder.Services.AddScoped<IStageStatutDataService, StageStatutDataService>();
builder.Services.AddScoped<IEtudiantDataService, EtudiantDataService>();
builder.Services.AddScoped<IEntrepriseDataService, EntrepriseDataService>();

await builder.Build().RunAsync();
