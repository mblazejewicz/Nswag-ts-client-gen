<template>
  <div class="about">
    <h1>This is an about page</h1>

  </div>
</template>

<script setup lang="ts">
import {  onMounted, ref } from 'vue';
import {Client, type WeatherForecast} from '../clients'

const weatherData = ref(0);

onMounted(async () => {
  const weatherClient = new Client(/* baseUrl */ "http://localhost:5271");
      const forecast = await weatherClient.getWeatherForecast(2,10);
      
console.log(forecast);
const newforecast = await weatherClient.createWeatherForecast({ temperatureC:666 } as WeatherForecast);
      console.log(newforecast);
})
</script>
<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
