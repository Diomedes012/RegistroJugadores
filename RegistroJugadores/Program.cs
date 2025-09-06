using RegistroJugadores.Components;
using RegistroJugadores.DAL;
using RegistroJugadores.Services;
using Microsoft.EntityFrameworkCore;

namespace RegistroJugadores
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            builder.WebHost.UseUrls($"http://*:{port}");


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //Obtener el ConStr para usarlo en el contexto
            var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

            //Agregamos el contexto al builder con el ConStr
            builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

            //Inyeccion del service
            builder.Services.AddScoped<JugadoresService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
