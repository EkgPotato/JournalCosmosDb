<script setup lang="ts">
import Title from '@/components/Title.vue';
import EntriesDataGrid from '@/components/EntriesDataGrid.vue';
import Loader from '@/components/base/Loader.vue';
</script>

<template>

  <header>
    <Title />
  </header>

  <main>
    <Loader />
    <EntriesDataGrid />
  </main>
</template>

<style scoped>
header {
  min-height: 5vh;
}

main {
  padding: 2rem;
  height: auto;
  width: 100%;
}
</style>
