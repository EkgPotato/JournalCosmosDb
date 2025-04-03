<script setup lang="ts">
import { ref, defineExpose, inject, computed } from 'vue';
import type { EntryService } from '@/services/EntryService';
import { useLoaderStore } from '@/stores/LoaderStore';
import type { EntryDTO } from '@/models/dtos/EntryDTO';
import Modal from '@/components/base/Modal.vue';
import NamedField from '@/components/base/NamedField.vue';
import TextInput from '@/components/base/input/TextInput.vue';

const title = ref<string>("")
const entryService = inject('entryService') as EntryService;
const entryToSave= ref<EntryDTO>({});
let entryId: string | undefined;
const isVisible = ref<boolean>(false)

const onClosed = () => {
    clear();
    isVisible.value = false
}

const open = async (id?: string) => {
    entryId = id;

    if (id) {
        title.value = "Edit journal entry";
        entryToSave.value = await useLoaderStore().runWithLoader<EntryDTO | undefined>( () => entryService.getEntry(id)) ?? {}
    }
    else {
        title.value = "New journal entry";
        entryToSave.value = {}
    }

    isVisible.value = true;
}

const onConfirmed = async () => {
    if (entryId) {
        await useLoaderStore().runWithLoader(() => entryService.updateEntry(entryId!, entryToSave.value))
    }
    else {
        await useLoaderStore().runWithLoader(() => entryService.createEntry(entryToSave.value))
    }

    emit("entrySavedSuccessfuly")
    clear();
    isVisible.value = false;
}

const clear = () => {
    entryToSave.value = {};
    entryId = undefined;
}

const onTitleChanged = (value?: string) => entryToSave.value.title = value
const onCategoryChanged = (value?: string) => entryToSave.value.category = value;
const onContentChanged = (value?: string) => entryToSave.value.content = value;

const isEntryValid = computed(() => entryToSave.value.title == null || entryToSave.value.title == undefined || entryToSave.value.title == "" )

const emit = defineEmits(["entrySavedSuccessfuly"])
defineExpose({ open })
</script>

<template>
    <Modal :isVisible="isVisible" :title="title" :is-confim-disabled="isEntryValid" @close="onClosed" @confirm="onConfirmed">
        <template #content>
            <div class="modal-content">
                <NamedField name="Tytuł">
                        <TextInput class="text-input" :value="entryToSave?.title" @value-changed="onTitleChanged"/>
                </NamedField>
                <NamedField name="Category">
                        <TextInput class="text-input" :value="entryToSave?.category" @value-changed="onCategoryChanged"/>
                </NamedField>
                <NamedField name="Content" flexDirection="column">
                        <TextInput class="content-input" :value="entryToSave?.content" :multiline="true" @value-changed="onContentChanged"/>
                </NamedField>
            </div>
        </template>
    </Modal>
</template>

<style scoped>
.modal-content {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    width: 800px;
    height: 600px;
}

.text-input {
    width: 500px;
}

.content-input {
    height: 400px;
}

</style>