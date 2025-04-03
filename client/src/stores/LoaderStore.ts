import { defineStore } from "pinia";

export const useLoaderStore = defineStore('loaderStore', {
    state: () => ({
        isLoading: false
    }),
    actions: {
        showLoader() {
          this.isLoading = true;
        },
        hideLoader() {
          this.isLoading = false;
        },
        async runWithLoader<T>(asyncFn: () => Promise<T>): Promise<T> {
            this.showLoader();
            try {
              const result = await asyncFn();
              return result;
            } finally {
              this.hideLoader();
            }
          }
      }
})