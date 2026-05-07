using MicroservicesSimulator.Hubs;
using MicroservicesSimulator.Services;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DE PORTA PARA HOSPEDAGEM ---
// O Render e outros serviços definem uma porta automática na variável de ambiente "PORT"
var port = Environment.GetEnvironmentVariable("PORT") ?? "5003";

// --- SERVIÇOS ---

builder.Services.AddSignalR();
builder.Services.AddHostedService<StatusSimulator>();

// Configuração de CORS atualizada para aceitar Localhost e Produção
builder.Services.AddCors(options => {
    options.AddPolicy("VueCorsPolicy", policy => {
        policy.SetIsOriginAllowed(origin => {
            // Permite localhost e qualquer URL da vercel.app (onde seu front ficará)
            return origin.Contains("localhost") || origin.Contains("vercel.app");
        })
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

// --- MIDDLEWARE ---

app.UseCors("VueCorsPolicy");

app.MapGet("/", () => "🚀 API do Simulador de Microsserviços está ativa. Conecte-se ao SignalR no /statusHub.");

app.MapHub<MonitoringHub>("/statusHub");

// IMPORTANTE: Mudamos de localhost fixo para 0.0.0.0 para que o servidor externo consiga acessar
app.Run($"http://0.0.0.0:{port}");