using Microsoft.AspNetCore.SignalR;

namespace MicroservicesSimulator.Hubs;

public class MonitoringHub : Hub 
{
    // Este método permite que o cliente peça uma atualização imediata se quiser
    public async Task RequestUpdate()
    {
        // Lógica opcional aqui
    }
}