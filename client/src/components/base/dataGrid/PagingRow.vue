<script setup lang="ts">
import { computed, ref } from 'vue';
import Lookup from '@/components/base/Lookup.vue';
import type { PageRequest } from '@/models/common/requests/PageRequest';
import { CalculatePageCount } from '@/utils/Utils';
import Selector from '@/components/base/selector/Selector.vue';

const props =  defineProps<{totalItems: number, pageRequest: PageRequest}>()

const pagingInfo = computed(() => `Page ${pageRequest.value.pageNumber} of ${availablePages.value} (${props.totalItems})`)

const emit =  defineEmits(["pagingChanged"]);

const pageRequest = ref<PageRequest>(props.pageRequest)

const onPageSizeChange = (pageSize: number) => {
  pageRequest.value.pageSize = pageSize;
  if (pageRequest.value.pageNumber > availablePages.value) {
    pageRequest.value.pageNumber = availablePages.value;
  }
  emit("pagingChanged", pageRequest.value);
};

const onPageNumberChanged = (pageNumber: number) => {
    pageRequest.value.pageNumber = pageNumber;
    emit("pagingChanged", pageRequest)
}

const availablePages = computed(() => CalculatePageCount(props.totalItems, pageRequest.value.pageSize))
const getPages = computed(() => {
    const pages = [];
    for(let i = 1; i <= availablePages.value; i++) {
        pages.push(i);
    }
    return pages;
})
</script>

<template>
    <div class="paging-row-wrapper">
        <Lookup :items="[10, 20, 50, 100]" :value="pageRequest.pageSize" @selected-change="onPageSizeChange"></Lookup>
        <p>{{ pagingInfo }}</p>
        <Selector :items="getPages" :selected="pageRequest.pageNumber" @selection-changed="onPageNumberChanged"/>
    </div>
</template>

<style scoped>

.paging-row-wrapper {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.paging-row-wrapper > p {
    margin: auto;
    font-size: 1.1rem;
    color: var(--color-text);
    font-weight: bold;
}
</style>

