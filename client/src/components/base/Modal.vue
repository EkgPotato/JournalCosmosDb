<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import IconButton from '@/components/base/button/IconButton.vue';

defineProps<{
    title: string,
    isVisible: boolean,
    isConfimDisabled?: boolean
}>()

const emit = defineEmits(["close", "confirm"])

</script>

<template>
    <div v-if="isVisible" class="modal-container">
        <div class="modal">
            <header class="modal-header">
                <p>{{ title }}</p>
            </header>

            <section class="modal-body">
                <slot name="content" />
            </section>

            <footer class="modal-footer">
                <IconButton text="Zamknij" icon="close" @click="emit('close')" color="negative" />
                <IconButton text="Zapisz" icon="confirm" @click="emit('confirm')" color="positive"
                    :disabled="isConfimDisabled" />
            </footer>
        </div>
    </div>
</template>

<style>
@import '@/assets/base.css';


.modal-container {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal {
    background: var(--color-background);
    box-shadow: 2px 2px 20px 1px;
    overflow-x: auto;
    display: flex;
    min-height: 500px;
    min-width: 400px;
    height: fit-content;
    width: fit-content;
    flex-direction: column;
}

.modal-header,
.modal-footer {
    padding: 0.5rem;
    display: flex;
}

.modal-header {
    position: relative;
    border-bottom: 1px solid #eeeeee;
    justify-content: space-between;
}

.modal-header p {
    font-size: 2em;
    font-weight: bold;
    width: 100%;
    text-align: center;
    padding: 0.2rem;
}

.modal-footer {
    border-top: 1px solid #eeeeee;
    flex-direction: row;
    justify-content: flex-end;
}

.modal-body {
    flex: 1 1 auto;
    position: relative;
    padding: 0.5rem
}
</style>