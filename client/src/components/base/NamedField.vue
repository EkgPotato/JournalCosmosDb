<template>
    <div class="field-container" :style="{ 'flex-direction': flexDirection }">
        <div class="field-name">
            {{ name }}
        </div>
        <div class="dx-field-value field-content">
            <slot />
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue';

withDefaults(
    defineProps<{
        name: string,
        flexDirection?: "row" | "column"
    }>(),
    {
        flexDirection: "row"
    }
)
</script>

<style scoped>
@import '@/assets/base.css';

.field-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
    gap: 0.5rem;
}

.field-content {
    width: auto;
}

.field-name {
    font-size: 1.2rem;
    color: var(--color-text);
    font-weight: bold;
    align-self: center;
    width: auto !important;
}
</style>