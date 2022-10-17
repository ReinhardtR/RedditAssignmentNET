using BlazorWasm;
using BlazorWasm.Auth;
using Domain.Auth;
using HttpClients.Clients;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<IPostsService, PostsService>();

AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddScoped(sp => new HttpClient());

builder.Services.AddAuthorizationCore();

builder.Services.AddMudServices();

await builder.Build().RunAsync();