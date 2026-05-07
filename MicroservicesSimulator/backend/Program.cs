using MicroservicesSimulator.Hubs;
using MicroservicesSimulator.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SERVIÇOS ---

builder.Services.AddSignalR();
builder.Services.AddHostedService<StatusSimulator>();

// Configure a política com nome para garantir a persistência
builder.Services.AddCors(options => {
    options.AddPolicy("VueCorsPolicy", policy => {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// --- MIDDLEWARE ---

// Use a política que você acabou de definir acima
app.UseCors("VueCorsPolicy");

app.MapGet("/", () => "API do Simulador de Microsserviços está ativa. Ligue-se ao SignalR no /statusHub.");

app.MapHub<MonitoringHub>("/statusHub");

// APENAS UM app.Run aqui, definindo a porta 5003
app.Run("http://localhost:5003");