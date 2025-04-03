<template>
    <div class="selector-wrapper">
        <SelectorButton :disabled="!hasPrevious" @clicked="selectPrevious" text="&lt;" />

        <span v-for="(item, index) in elements" :key="index">
            <SelectorButton v-if="item !== '...'" :selected="isSelected(item)" @clicked="selectItemByValue(item)"
                :text="item.toString()" />
            <span v-else class="ellipsis">...</span>
        </span>

        <SelectorButton :disabled="!hasNext" @clicked="selectNext" text="&gt;" />
    </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits, computed, watch, onMounted } from 'vue';
import SelectorButton from '@/components/base/selector/SelectorButton.vue';

const props = defineProps<{
    items: (string | number)[],
    selected: string | number
}>()

const emit = defineEmits(['selectionChanged']);

const currentSelection = ref<string | number>(props.selected);

onMounted(() => {
    watch(
        () => props.selected,
        (newVal) => currentSelection.value = newVal
    );
})

const selectedIndex = computed(() =>props.items.findIndex(item => item === currentSelection.value));
const hasPrevious = computed(() => selectedIndex.value > 0);
const hasNext = computed(() => selectedIndex.value < props.items.length - 1);

const elements = computed<(string | number)[]>(() => {
    const items = props.items;
    const count = items.length;
    const selIndex = items.findIndex(item => item === currentSelection.value);
    const result: (string | number)[] = [];

    if (count <= 5) {
        return [...items];
    } else {
        if (selIndex <= 2) {
            result.push(items[0], items[1], items[2], items[3], '...', items[count - 1]);
        } else if (selIndex >= count - 3) {
            result.push(items[0], '...', items[count - 4], items[count - 3], items[count - 2], items[count - 1]);
        } else {
            result.push(
                items[0],
                '...',
                items[selIndex - 1],
                items[selIndex],
                items[selIndex + 1],
                '...',
                items[count - 1]
            );
        }
    }
    return result;
});

const isSelected = (item: string | number): boolean => {
    return item === currentSelection.value;
};

const selectItemByValue = (item: string | number): void => {
    if (item === '...') return; 
    currentSelection.value = item;
    emit('selectionChanged', currentSelection.value);
};

const selectPrevious = (): void => {
    if (hasPrevious.value) {
        currentSelection.value = props.items[selectedIndex.value - 1];
        emit('selectionChanged', currentSelection.value);
    }
};

const selectNext = (): void => {
    if (hasNext.value) {
        currentSelection.value = props.items[selectedIndex.value + 1];
        emit('selectionChanged', currentSelection.value);
    }
};

</script>

<style scoped>
.selector-wrapper {
    display: flex;
    align-items: center;
    gap: 8px;
}

.ellipsis {
    padding: 0.5rem 1rem;
    font-size: 1rem;
    color: var(--color-text);
}
</style>