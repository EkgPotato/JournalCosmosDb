<template>
  <div class="data-grid">
    <table>
      <thead>
        <tr>
          <th :colspan="columns.length">
            <TextInput placeholder="Search" @value-changed="onSearchChanged" additional-icon="search" />
          </th>
          <th class="header-action">
            <div class="option-cell">
              <slot name="headerActions" />
              <IconButton icon="refresh" @clicked="onRefresh" />
            </div>
          </th>
        </tr>
        <tr>
          <template v-for="(col, colIndex) in columns" :key="colIndex">
            <th>
              <SortableHeader v-if="col.sortable" :caption="col.title"
                @sortChanged="(order) => onSortChanged(col.field, order)" />
              <span v-else>{{ col.title }}</span>
            </th>
          </template>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <template v-for="(row, rowIndex) in data" :key="rowIndex">
          <tr @click="toggleRow(row)" class="data-row">
            <template v-for="(col, colIndex) in columns" :key="colIndex">
              <td>
                <slot :name="`cell-${col.field}`" :row="row">
                  {{ row[col.field] }}
                </slot>
              </td>
            </template>
            <td v-if="hasActions" class="option-cell-container">
              <div class="option-cell">
                <slot name="rowActions" :row="row" />
              </div>
            </td>
          </tr>
          <tr v-if="isRowExpanded(row)" :key="rowIndex + '-detail'">
            <td :colspan="columns.length + (hasActions ? 1 : 0)" class="detail-cell">
              <slot name="rowDetail" :row="row" />
            </td>
          </tr>
        </template>
        <tr>
          <td :colspan="columns.length + (hasActions ? 1 : 0)">
            <PagingRow :total-items="totalItems" :page-request="gridRequest.paging" @paging-changed="onPagingChanged" />
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits, defineExpose, onMounted, watch } from 'vue';
import IconButton from '@/components/base/button/IconButton.vue';
import TextInput from '@/components/base/input/TextInput.vue';
import PagingRow from '@/components/base/dataGrid/PagingRow.vue';
import SortableHeader from '@/components/base/dataGrid/SortableHeader.vue';
import type { Column } from '@/models/common/Column';
import type { GridRequest } from '@/models/common/requests/GridRequest';
import type { PageRequest } from '@/models/common/requests/PageRequest';
import { SortOrderEnum } from '@/models/common/SortOrderEnum';

const props = defineProps<{
  columns: Column[],
  data: any[],
  totalItems: number,
  hasActions?: boolean,
}>();

const emit = defineEmits(["requestChanged", "refreshInvoked"]);

const gridRequest = ref<GridRequest>({
  paging: {
    pageNumber: 1,
    pageSize: 20
  },
  sorts: []
})

const expandedRows = ref<Set<string | number>>(new Set());

let requestChangeTimeout: ReturnType<typeof setTimeout>;

const toggleRow = (row: any) => {
  const id = row.id;
  if (!id) return;

  if (expandedRows.value.has(id)) {
    expandedRows.value.delete(id);
  } else {
    expandedRows.value.add(id);
  }
}

const isRowExpanded = (row: any): boolean => {
  return row.id ? expandedRows.value.has(row.id) : false;
}

const onSortChanged = (field: string, order: SortOrderEnum) => {
  const sortCollection = gridRequest.value.sorts;
  const existingIndex = sortCollection.findIndex(s => s.field === field);
  const exists = existingIndex !== -1;

  if (order === SortOrderEnum.none) {
    if (exists) sortCollection.splice(existingIndex, 1);
  } else if (exists) {
    sortCollection[existingIndex].order = order;
  } else {
    sortCollection.push({ field, order });
  }

  emitRequestChanged();
}

const onPagingChanged = (paging: PageRequest) => {
  gridRequest.value.paging = paging;
  emitRequestChanged();
}

const onSearchChanged = (search: string) => {
  if (search) {
    gridRequest.value.search = undefined;
  }
  else {
    gridRequest.value.search = search
  }

  emitRequestChanged();
}

const onRefresh = () => emit("refreshInvoked", gridRequest.value)
const refresh = () => onRefresh();

const emitRequestChanged = () => {
  clearTimeout(requestChangeTimeout);
  requestChangeTimeout = setTimeout(() => {
    emit('requestChanged', gridRequest.value);
  }, 500);
};

defineExpose({ refresh })
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
  table-layout: auto;
}

thead th {
  color: var(--color-contrast-text);
  background-color: var(--color-header);
  font-weight: bold;
  font-size: 1.2rem;
  padding: 8px 12px;
  border: 1px solid var(--color-border);
  text-align: center;
  white-space: nowrap;
}

thead th.header-action {
  padding: 4px 8px;
  white-space: nowrap;
  min-width: min-content;
  text-align: center;
}

tbody td {
  padding: 4px 8px;
  border: 1px solid var(--color-border);
  word-wrap: break-word;
}

.option-cell {
  display: inline-block;
  min-width: min-content;
  padding: 2px 4px;

}

tbody td.option-cell-container {
  width: 1%;
}

tbody .data-row:nth-child(even) {
  background: var(--color-field);
}

tbody .data-row:nth-child(odd) {
  background: var(--color-background);
}

tbody .data-row:hover {
  background-color: var(--main-selection-soft-color);
}

.detail-cell {
  padding: 1.5rem;
  word-wrap: break-word;
  overflow-wrap: break-word;
}
</style>
