<template>
    <transition name="fade">
        <div v-if="store.isLoading" class="overlay">
            <div class="spinner"></div>
        </div>
    </transition>
</template>

<script setup lang="ts">
import { useLoaderStore } from "@/stores/LoaderStore"

const store = useLoaderStore();
</script>

<style scoped>
.overlay {
    position: fixed;
    top: 0;
    left: 0;
    z-index: 9999;
    width: 100vw;
    height: 100vh;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
}

.spinner {
    width: 50px;
    height: 50px;
    border: 6px solid #f3f3f3;
    border-top: 6px solid #3498db;
    border-radius: 50%;
    animation: spin 1s linear infinite;
}

/* Animacja obrotu */
@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>