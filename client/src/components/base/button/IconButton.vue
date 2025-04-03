<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { defineProps, defineEmits, computed } from 'vue';
import { faPlus, faArrowsRotate, faPen, faTrashCan, faExpand, faMinimize, faArrowPointer, faCircleXmark, faCircleCheck, type IconDefinition } from '@fortawesome/free-solid-svg-icons';

type iconType = "refresh" | "add" | "edit" | "delete" | "expand" | "shrink" | "close" | "confirm" | "default"
type colorType = "negative" | "default" | "positive"

const props = withDefaults(
  defineProps<{
    text?: string,
    icon?: iconType,
    disabled?: boolean,
    color?: colorType
  }>(),
  {
    icon: "default",
    color: "default",
    disabled: false
  });

const emit = defineEmits(["clicked"])

const mapToFontAwesome = (icon?: iconType): IconDefinition => {
  switch (icon) {
    case 'refresh': return faArrowsRotate;
    case 'add': return faPlus;
    case 'edit': return faPen;
    case 'delete': return faTrashCan;
    case 'expand': return faExpand;
    case 'shrink': return faMinimize;
    case 'close': return faCircleXmark;
    case 'confirm': return faCircleCheck;
    default:
      return faArrowPointer;
  }
}

const computedStyleClass = computed(() => {
  if (props.disabled) {
    return 'icon-button-disabled'
  }
  switch (props.color) {
    case 'negative':
      return 'icon-button-negative'
    case 'positive':
      return 'icon-button-positive'
    default:
      return 'icon-button-default'
  }
})

</script>

<template>
  <button class="icon-button" @click.stop="emit('clicked')" :disabled="disabled" :class="[computedStyleClass]">
    <FontAwesomeIcon :icon="mapToFontAwesome(icon)" />
    <span v-if="text" class="icon-text">{{ text }}</span>
  </button>
</template>

<style scoped>
@import '@/assets/base.css';

.icon-button {
  font-size: 1.2rem;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 8px;
  border: none;
  border-radius: 5px;
  margin: 2px;
  color: var(--color-contrast-text);
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.icon-button:not[disabled] :hover {
  background-color: var(--main-selection-color);
}

.icon-button-disabled {
  background-color: var(--color-border);
  color: var(--color-contrast-text);
  cursor: not-allowed;
}

.icon-button-negative {
  background-color: var(--color-negative);
}

.icon-button-positive {
  background-color: var(--color-positive);
}

.icon-button-default {
  background-color: var(--main-action-color);
}

.icon-button:focus {
  outline: none;
  box-shadow: 0 0 0 2px rgba(52, 152, 219, 0.3);
}

.icon-text {
  margin-left: 6px;
  font-size: 1.2rem;
}
</style>