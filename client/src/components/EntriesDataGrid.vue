<template>
    <DataGrid ref="gridRef" :columns="columns" :data="data" :totalItems="totalItemsCount" :hasActions="true"
        @request-changed="onRequestChanged" @refresh-invoked="onRefreshInvoked">
        <template #cell-title="{ row }">
            <strong>{{ row.title }}</strong>
        </template>
        <template #rowActions="{ row }">
            <IconButton icon="edit" @clicked="onEdit(row.id)" />
            <IconButton icon="delete" @clicked="onDelete(row.id)" />
        </template>
        <template #rowDetail="{ row }">
            <p>{{ row.content }}</p>
        </template>
        <template #headerActions>
            <IconButton icon="add" @clicked="onAdd" />
        </template>
    </DataGrid>
    <EntriesModal ref="modalRef" @entry-saved-successfuly="forceRefresh()" />
</template>

<script setup lang="ts">
import { ref, inject, onMounted } from 'vue';
import DataGrid from '@/components/base/dataGrid/DataGrid.vue';
import IconButton from '@/components/base/button/IconButton.vue';
import type { Column } from '@/models/common/Column';
import type { EntryViewModel } from '@/models/viewModels/EntryViewModel';
import type { GridRequest } from '@/models/common/requests/GridRequest';
import type { EntryService } from '@/services/EntryService';
import { useLoaderStore } from '@/stores/LoaderStore';
import EntriesModal from '@/components/EntriesModal.vue';
import type { PagedResponse } from '../models/common/PagedResponse';

const gridRef = ref<InstanceType<typeof DataGrid> | null>(null);
const modalRef = ref<InstanceType<typeof EntriesModal> | null>(null);

const columns: Column[] = [
    { title: 'Title', field: 'title', sortable: true },
    { title: 'Category', field: 'category', sortable: true },
    { title: 'Creation time', field: 'createdAt', sortable: true },
    { title: 'Updated time', field: 'updatedAt', sortable: true },
];

const data = ref<EntryViewModel[]>([]);
const totalItemsCount = ref<number>(0)
const entryService = inject('entryService') as EntryService;

onMounted(() => gridRef?.value?.refresh())

const onRequestChanged = async (request: GridRequest) => await fetchData(request);
const onRefreshInvoked = async (request: GridRequest) => await fetchData(request);
const fetchData = async (request: GridRequest) => {
    const result = await useLoaderStore().runWithLoader<PagedResponse<EntryViewModel>>(() => entryService.getEntries(request));
    data.value = result.items;
    totalItemsCount.value = result.totalItemsCount;
}

const onAdd = () => modalRef.value?.open();
const onEdit = (id: string) => modalRef.value?.open(id);

const onDelete = async (id: string) => {
    await useLoaderStore().runWithLoader(() => entryService.deleteEntry(id))
    forceRefresh();
}

const forceRefresh = () => gridRef.value?.refresh();
</script>