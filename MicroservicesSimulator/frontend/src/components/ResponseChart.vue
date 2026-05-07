<template>
  <div class="chart-container">
    <Line :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { ref, shallowRef, watch } from 'vue';
import { Line } from 'vue-chartjs';
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Filler } from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Filler);

const props = defineProps(['latency', 'status']);
const dataPoints = ref(Array(20).fill(0));

const getNeonColor = () => {
  if (props.status === 'Warning') return '#ffcc00';
  if (props.status === 'Down') return '#ff4444';
  return '#00e5ff'; // Healthy usa Ciano Neon no gráfico para contraste
};

const chartData = shallowRef({
  labels: Array(20).fill(''),
  datasets: [{
    data: [...dataPoints.value],
    borderColor: getNeonColor(),
    borderWidth: 2,
    tension: 0.4,
    fill: true,
    backgroundColor: 'rgba(0, 229, 255, 0.05)',
    pointRadius: 0
  }]
});

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  plugins: { legend: { display: false } },
  scales: { y: { display: false, min: 0, max: 600 }, x: { display: false } },
  animation: { duration: 300 }
};

watch(() => props.latency, (newVal) => {
  dataPoints.value.push(newVal);
  dataPoints.value.shift();
  
  chartData.value = {
    ...chartData.value,
    datasets: [{
      ...chartData.value.datasets[0],
      borderColor: getNeonColor(),
      backgroundColor: `rgba(${props.status === 'Warning' ? '255,204,0' : props.status === 'Down' ? '255,68,68' : '0,229,255'}, 0.03)`,
      data: [...dataPoints.value]
    }]
  };
});
</script>

<style scoped>
/* No ResponseChart.vue */
.chart-container { 
  height: 80px; /* Aumentar de 60px para 80px para preencher melhor o fundo */
  width: 100%; 
  opacity: 0.7; /* Dar uma leve transparência para não brigar com o texto */
}
</style>