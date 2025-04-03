<template>
    <div class="input-wrapper">
        <FontAwesomeIcon v-if="additionalIcon" class="additional-icon"  :icon="mapToFontAwesome(additionalIcon)" />

        <!-- Text input -->
        <textarea v-if="multiline" type="text" v-model="inputValue" :placeholder="placeholder"
            @change="onValueChanged"></textarea>
        <input v-else v-model="inputValue" type="text" :placeholder="placeholder" @change="onValueChanged"/>

        <!-- Clear icon on the right, only shown when there is text -->
        <FontAwesomeIcon v-if="inputValue && clear" :icon="faCircleXmark" class="clear-icon" @click="clearInput" />
    </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from 'vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faCircleXmark, faMagnifyingGlass, faPen, type IconDefinition } from '@fortawesome/free-solid-svg-icons';

const props = withDefaults(defineProps<{
    value?: string;
    placeholder?: string;
    clear?: boolean;
    additionalIcon?: "search";
    multiline?: boolean
}>(),
{
    clear: true,
    multiline: false
})

const emit = defineEmits(['valueChanged']);

const inputValue = ref<string | undefined>(props.value);

const onValueChanged = () => emit('valueChanged', inputValue.value);

const clearInput = () => {
    inputValue.value = undefined;
    emit('valueChanged', null);
}

const mapToFontAwesome = (icon: string): IconDefinition => {
    switch (icon) {
        case 'search': return faMagnifyingGlass;
        default: return faPen;
    }
}
</script>

<style scoped>
@import '@/assets/base.css';

.input-wrapper {
    position: relative;
    display: flex;
    align-items: center;
    width: 100%;
}

.input-wrapper input,
.input-wrapper textarea
 {
    height: 100%;
    width: 100%;
    padding: 8px 36px;
    font-size: 1rem;
    border: 1px solid var(--color-border);
    border-radius: 4px;
    box-sizing: border-box;
}

.input-wrapper input:focus,
.input-wrapper textarea:focus {
  outline: none;
  border-color: var(--main-selection-color); 
  border-width: 2px;
}

.input-wrapper input:hover:not(:focus),
.input-wrapper textarea:hover:not(:focus) {
  border-color: var(--main-selection-soft-color);
}

/* Position the search icon on the left */
.additional-icon {
    position: absolute;
    left: 10px;
    color: var(--color-border);
    font-size: 1.2rem;
}

/* Position the clear icon on the right */
.clear-icon {
    position: absolute;
    right: 10px;
    color: var(--color-border);
    font-size: 1.2rem;
    cursor: pointer;
    transition: color 0.2s ease;
}

.clear-icon:hover {
    color: var(--main-selection-color);
}
</style>