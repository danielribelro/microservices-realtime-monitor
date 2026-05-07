using Microsoft.AspNetCore.SignalR;
using MicroservicesSimulator.Hubs;
using MicroservicesSimulator.Models;

namespace MicroservicesSimulator.Services;

public class StatusSimulator : BackgroundService
{
    private readonly IHubContext<MonitoringHub> _hubContext;
    private readonly Random _random = new();

    public StatusSimulator(IHubContext<MonitoringHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var services = new List<string> { "Auth-API", "Payment-Gateway", "Inventory-Service", "Order-Worker" };

        while (!stoppingToken.IsCancellationRequested)
        {
            var data = services.Select(s => new ServiceStatus
            {
                Id = s.ToLower(),
                Name = s,
                Latency = _random.Next(20, 500),
                Status = _random.Next(0, 10) > 8 ? "Warning" : "Healthy",
                LastUpdate = DateTime.Now
            }).ToList();

            // Envia para TODOS os clientes conectados via WebSocket
            await _hubContext.Clients.All.SendAsync("ReceiveStatus", data, stoppingToken);

            await Task.Delay(2000, stoppingToken); // Atualiza a cada 2 segundos
        }
    }
}