<template>
  <div class="app-shell">
    <div class="sidebar">
      <div class="brand">
        <div class="logo-text">M::</div>
        <h1>CONTROL::<span>CENTER</span></h1>
      </div>
      
      <div class="controls">
        <div class="control-item system-status" :class="{ connected: connected }">
          <span class="dot"></span>
          {{ connected ? 'ACTIVE' : 'CONNECTING...' }}
        </div>
      </div>
    </div>

    <div class="main-content">
      <div class="dashboard-grid">
        <StatusCard 
          v-for="s in services" 
          :key="s.id" 
          v-bind="s" 
        />
      </div>

      <footer class="console-wrapper">
        <LogViewer :logs="logs" />
      </footer>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import * as signalR from '@microsoft/signalr';
import StatusCard from './components/StatusCard.vue';
import LogViewer from './components/LogViewer.vue';

const services = ref([]);
const logs = ref([]);
const connected = ref(false);

onMounted(() => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5003/statusHub")
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveStatus", (data) => {
    services.value = data;
    connected.value = true;

    data.forEach(s => {
      if (s.status !== "Healthy") {
        logs.value.push({
          time: new Date().toLocaleTimeString('pt-BR', { hour12: false }),
          text: `Critical: ${s.name} latency at ${s.latency}ms`,
          type: s.status === 'Down' ? 'error' : 'warning'
        });
      }
    });
    if (logs.value.length > 50) logs.value.shift();
  });

  connection.start();
});
</script>

<style scoped>
.app-shell { display: flex; min-height: 100vh; background: var(--bg-deep); }

/* Sidebar */
.sidebar { 
  width: 250px; background: var(--bg-panel); 
  border-right: 1px solid var(--border-subtle); 
  padding: 1.5rem; display: flex; flex-direction: column; 
}
.brand { display: flex; flex-direction: column; margin-bottom: 2rem; border-bottom: 1px solid var(--border-subtle); padding-bottom: 1rem; }
.logo-text { font-family: 'Fira Code', monospace; font-size: 1.5rem; font-weight: 800; color: var(--neon-primary); margin-bottom: 0.5rem; }
h1 { margin: 0; font-size: 1.2rem; font-weight: 800; text-transform: uppercase; letter-spacing: -0.05em; }
h1 span { color: var(--neon-primary); font-weight: 400; }

.controls { margin-top: auto; }
.control-item { display: flex; align-items: center; gap: 10px; font-size: 0.8rem; color: var(--text-muted); }
.control-item .dot { width: 8px; height: 8px; border-radius: 50%; background: var(--border-subtle); }

.control-item.connected { color: var(--neon-success); font-weight: bold; }
.control-item.connected .dot { background: var(--neon-success); box-shadow: 0 0 10px var(--neon-success); }

/* Conteúdo Principal */
.main-content { flex: 1; padding: 1.5rem; display: flex; flex-direction: column; }
/* No App.vue */
.dashboard-grid { 
  display: grid; 
  /* Mude de 260px para 300px para cards mais robustos */
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); 
  gap: 1rem; 
  align-content: start; /* Alinha os cards no topo */
}

.console-wrapper { margin-top: auto; padding-top: 1.5rem; border-top: 1px solid var(--border-subtle); }
</style>