using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicTacToe.Services;

namespace TicTacToe
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/") });

            builder.Services.AddScoped<IJugadoresApiService, JugadoresApiService>();
            builder.Services.AddScoped<IPartidaApiService, PartidaApiService>();
            builder.Services.AddScoped<IMovimientosApiService, MovimientosApiService>();

            await builder.Build().RunAsync();
        }
    }
}
