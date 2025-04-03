<template>
    <button :class="['selector-button', { selected: selected }]" :disabled="disabled" @click="emit('clicked')">
        {{ text }}
    </button>
</template>

<script setup lang="ts">
defineProps<{
    text: string | number
    selected?: boolean,
    disabled?: boolean
}>();

const emit = defineEmits(['clicked'])
</script>

<style scoped>
@import '@/assets/base.css';

.selector-button {
    padding: 0.5rem 1rem;
    border: 1px solid var(--color-border);
    border-radius: 4px;
    background: var(--color-background);
    cursor: pointer;
    transition: background-color 0.2s ease;
}

.selector-button:hover:not(.selected) {
    background-color: var(--main-selection-soft-color);
}

.selector-button.selected {
    background-color: var(--main-selection-color);
    color: var(--color-contrast-text);
    border-color: var(--main-selection-color);
    cursor: default;
}

.selector-button:disabled {
    opacity: 0.5;
    cursor: not-allowed;
}
</style>