<template>
  <div class="sortable-header" @click="toggleSort">
    <span class="caption">{{ caption }}</span>
    <FontAwesomeIcon v-if="order !== SortOrderEnum.none" :icon="icon!" class="sort-icon" />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, defineEmits } from 'vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faArrowDown, faArrowUp } from '@fortawesome/free-solid-svg-icons';
import { SortOrderEnum } from '../../../models/common/SortOrderEnum';


const props = withDefaults(
  defineProps<
    {
      caption: string,
      initialOrder?: SortOrderEnum
    }>(),
  {
    initialOrder: SortOrderEnum.none
  })

const emit = defineEmits(["sortChanged"])

const order = ref<SortOrderEnum>(props.initialOrder);

const icon = computed(() => {
  switch (order.value) {
    case SortOrderEnum.desc: return faArrowDown;
    case SortOrderEnum.asc: return faArrowUp;
    default: return null;
  }
});

const toggleSort = () => {
  if (order.value === SortOrderEnum.none) {
    order.value = SortOrderEnum.desc;
  } else if (order.value === SortOrderEnum.desc) {
    order.value = SortOrderEnum.asc;
  } else {
    order.value = SortOrderEnum.none;
  }

  emit('sortChanged', order.value);
}
</script>

<style scoped>
.sortable-header {
  width: 100%;
  display: flex;
  justify-content: space-between;
  align-items: center;
  cursor: pointer;
  user-select: none;
}

.caption {
  margin-right: 4px;
  font-weight: bold;
}
</style>