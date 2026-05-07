namespace MicroservicesSimulator.Models;

public class ServiceStatus
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; } // Ex: "Online", "Offline", "Degraded"
    
    // Novas propriedades para suportar o simulador
    public int Latency { get; set; } // Tempo de resposta em ms
    public DateTime LastUpdate { get; set; } // Carimbo de data/hora da última simulação
}