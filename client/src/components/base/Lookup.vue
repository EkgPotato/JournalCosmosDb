<template>
  <div class="lookup-select" @click.stop="toggleDropdown">
    <p class="selected-value">{{ selectedItem }}</p>
    <ul v-show="isOpen" class="options-list">
      <li v-for="(item, index) in items" :key="index" @click.stop="selectItem(item)">
        {{ item }}
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue';

const props = defineProps<{
  items: any[]
  value?: any
}>()

const emit = defineEmits(["selectedChange"])

const selectedItem = ref<any>(props.value)

const isOpen = ref<boolean>(false);

const toggleDropdown = () => isOpen.value = !isOpen.value;

const selectItem = (item: any) => {
  selectedItem.value = item;
  emit('selectedChange', item);
  isOpen.value = false;
}

const closeDropdown = (event: any) => {
  if (!event.target.closest('.lookup-select')) {
    isOpen.value = false;
  }
}

onMounted(() => document.addEventListener('click', closeDropdown));
onBeforeUnmount(() => document.removeEventListener('click', closeDropdown));

</script>

<style scoped>
.lookup-select {
  position: relative;
  width: 200px;
  cursor: pointer;
  border: 1px solid var(--color-border);
  border-radius: 4px;
  padding: 8px;
  background: var(--color-background);
}

.lookup-select:hover {
  background-color: var(--main-selection-soft-color);;
}


.options-list {
  position: absolute;
  top: 100%;
  left: 0;
  width: 100%;
  margin: 0;
  padding: 0;
  list-style: none;
  border: 1px solid var(--color-border);
  border-top: none;
  border-radius: 0 0 4px 4px;
  background: var(--color-background);
  z-index: 1000;
}

.options-list li {
  padding: 8px;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.options-list li:hover {
  background-color: var(--main-selection-soft-color);;
}
</style>