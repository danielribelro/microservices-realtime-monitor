# 🛰️ Microservices Mission Control Dashboard

Um painel de monitoramento em tempo real com estética Cyberpunk, desenvolvido para visualizar a saúde e a latência de uma arquitetura de microsserviços distribuídos.

![Dashboard Preview](https://github.com/seu-usuario/seu-repositorio/blob/main/caminho-da-sua-imagem.png?raw=true)
> *Dica: Após subir o projeto, arraste o print do seu dashboard para dentro do README no GitHub para gerar o link da imagem.*

## 🧪 Sobre o Projeto

Este projeto foi construído para demonstrar a integração de fluxos de dados em tempo real entre um ecossistema **Backend .NET** e um **Frontend reativo em Vue.js 3**, utilizando WebSockets para garantir atualização instantânea sem a necessidade de refresh.

## 🛠️ Stack Tecnológica

### Backend (Engine)
- **C# / .NET 8**: Web API robusta.
- **SignalR**: Comunicação bidirecional e WebSockets.
- **Worker Services**: Simulação de processos em background.

### Frontend (Interface)
- **Vue.js 3**: Composition API para reatividade.
- **Vite**: Build tool ultra-rápido.
- **Chart.js**: Renderização de gráficos de latência em tempo real.
- **CSS3 (Custom Theme)**: Design imersivo estilo "Command Center" com efeitos Neon/Glow.

## 🚀 Desafios Técnicos Superados

- **CORS Policy**: Configuração de segurança para permitir tráfego entre portas distintas.
- **Performance de Memória**: Implementação de `shallowRef` no Vue para lidar com objetos complexos do Chart.js, evitando estouro de pilha (call stack).
- **UX Reativa**: Sistema de logs que rola automaticamente e badges que pulsam de acordo com o status do serviço.

## 🏁 Como Rodar o Projeto

### 1. Clonar o Repositório
```bash
git clone [https://github.com/seu-usuario/microservices-realtime-monitor.git](https://github.com/seu-usuario/microservices-realtime-monitor.git)
