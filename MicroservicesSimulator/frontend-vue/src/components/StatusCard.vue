<template>
  <div class="card" :class="status.toLowerCase()">
    <div class="card-glow"></div>
    <div class="status-indicator">
      <span class="dot"></span>
      <span class="pulse"></span>
    </div>
    
    <div class="content">
      <h3>{{ name }}</h3>
      <span class="status-text">{{ status }}</span>
      
      <div class="metrics">
        <div class="latency">
          <span class="value">{{ latency }}</span>
          <span class="unit">ms</span>
        </div>
      </div>
    </div>

    <div class="chart-wrapper">
      <ResponseChart :latency="latency" :status="status" />
    </div>
  </div>
</template>

<script setup>
import ResponseChart from './ResponseChart.vue';
defineProps(['name', 'status', 'latency']);
</script>

<style scoped>
/* No StatusCard.vue */
.card {
  /* ... outros estilos */
  padding: 1rem; /* Reduzir de 1.2rem para 1rem */
  min-height: 220px; /* Adicionar uma altura mínima controlada */
}

.value { 
  font-size: 1.8rem; /* Reduzir levemente de 2.2rem para 1.8rem */
}
.card:hover {
  transform: translateY(-3px) scale(1.02);
  border-color: var(--border-highlight);
  box-shadow: 0 0 20px rgba(0, 229, 255, 0.2);
}

/* Indicador de Status com Glow Intenso */
.status-indicator {
  position: absolute; top: 1.2rem; right: 1.2rem;
  display: flex; align-items: center; justify-content: center;
}
.dot, .pulse { width: 10px; height: 10px; border-radius: 50%; position: absolute; }

/* Dinâmica de Cores Neon */
.healthy .dot { background: var(--neon-success); box-shadow: 0 0 10px var(--neon-success); }
.healthy .pulse { background: var(--neon-success); animation: pulse 1.5s infinite; opacity: 0.5; }

.warning .dot { background: var(--neon-warning); box-shadow: 0 0 10px var(--neon-warning); }
.down .dot { background: var(--neon-danger); box-shadow: 0 0 10px var(--neon-danger); }

@keyframes pulse {
  0% { transform: scale(1); opacity: 0.5; }
  100% { transform: scale(2.5); opacity: 0; }
}

/* Conteúdo */
.content h3 { margin: 0; font-size: 1rem; font-weight: 600; color: var(--text-main); }
.status-text { font-size: 0.7rem; text-transform: uppercase; letter-spacing: 0.1em; color: var(--text-muted); }

.metrics { margin-top: 1rem; }
.latency { display: flex; align-items: baseline; gap: 4px; }
.value { font-size: 2.2rem; font-weight: 800; font-variant-numeric: tabular-nums; }

/* Bordas Contínuas Neon */
.healthy { border-color: rgba(0, 255, 136, 0.3); }
.warning { border-color: rgba(255, 204, 0, 0.3); }
.down { border-color: rgba(255, 68, 68, 0.3); }

/* Cor da latência baseada no status */
.healthy .value { color: var(--neon-success); }
.warning .value { color: var(--neon-warning); }
.down .value { color: var(--neon-danger); }

.chart-wrapper { margin-top: auto; padding-top: 1rem; border-top: 1px solid var(--border-subtle); }
</style>