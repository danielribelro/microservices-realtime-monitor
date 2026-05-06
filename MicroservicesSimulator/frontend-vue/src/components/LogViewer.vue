<template>
  <div class="log-panel">
    <div class="log-header">
      <div class="meta">
        <span class="icon">SYS</span>
        <span class="title">CONSOLE::STREAM</span>
      </div>
    </div>
    <div class="log-body" ref="scrollBox">
      <div v-for="(log, i) in logs" :key="i" class="log-line" :class="log.type">
        <span class="time">{{ log.time }}</span>
        <span class="type-badge">{{ log.type === 'error' ? 'ERR' : log.type === 'warning' ? 'WARN' : 'INF' }}</span>
        <span class="message">{{ log.text }}</span>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, nextTick } from 'vue';
const props = defineProps(['logs']);
const scrollBox = ref(null);

watch(() => props.logs.length, async () => {
  await nextTick();
  if (scrollBox.value) scrollBox.value.scrollTop = scrollBox.value.scrollHeight;
});
</script>

<style scoped>
.log-panel {
  background: rgba(15, 23, 42, 0.9);
  border: 1px solid var(--border-subtle);
  border-radius: 8px;
  backdrop-filter: blur(5px);
  margin-top: 1.5rem;
}
.log-header {
  padding: 10px 15px;
  background: var(--bg-panel-light);
  border-bottom: 1px solid var(--border-subtle);
}
.icon { background: #334155; padding: 2px 5px; border-radius: 4px; font-weight: bold; margin-right: 8px; }
.title { color: var(--text-muted); font-size: 0.8rem; letter-spacing: 0.1em; font-family: 'Fira Code', monospace; }

.log-body {
  height: 160px;
  overflow-y: auto;
  padding: 12px;
  font-family: 'Fira Code', 'Courier New', monospace;
  font-size: 0.8rem;
  display: flex;
  flex-direction: column-reverse; /* Inverte a ordem para novos logs aparecerem no topo */
}

.log-line { display: flex; gap: 10px; margin-bottom: 6px; padding: 4px 8px; border-radius: 4px; border-left: 3px solid transparent; }
.time { color: var(--border-subtle); min-width: 75px; font-variant-numeric: tabular-nums; }
.message { color: var(--text-main); flex: 1; }

.type-badge { font-weight: bold; font-size: 0.7rem; min-width: 40px; text-align: center; color: var(--bg-deep); border-radius: 3px; }

/* Cores dos Logs baseadas no tipo com barra lateral neon */
.info .type-badge { background: #475569; }
.info { border-left-color: #334155; }

.warning .type-badge { background: var(--neon-warning); }
.warning .message { color: var(--neon-warning); font-weight: bold; }
.warning { border-left-color: var(--neon-warning); background: rgba(255, 204, 0, 0.05); }

.error .type-badge { background: var(--neon-danger); }
.error .message { color: var(--neon-danger); font-weight: bold; }
.error { border-left-color: var(--neon-danger); background: rgba(255, 68, 68, 0.05); }
</style>