// src/services/notificationService.ts

import { useToast, type ToastProps } from "vue-toast-notification";

export class NotificationService {
    private readonly _toast = useToast()
    private readonly _toastOptions: ToastProps = {
        position: "top",
        duration: 5000,
        dismissible: true,
    }
    private readonly messagesTitles = {
        success: "Success!",
        warning: "Warning!",
        error: "Error!",
        info: "Information!",
    }

    success(message: string): void {
        this._toast.success(this.createMessage(this.messagesTitles.success, message), this._toastOptions)
    }

    error(message: string, logMessage?: string): void {
        this._toast.error(this.createMessage(this.messagesTitles.error, message), this._toastOptions)
    }

    warning(message: string): void {
        this._toast.warning(this.createMessage(this.messagesTitles.warning, message), this._toastOptions)
    }

    info(message: string): void {
        this._toast.info(this.createMessage(this.messagesTitles.info, message), this._toastOptions)
    }

    default(message: string): void {
        this._toast.default(message, this._toastOptions);
    }

    private createMessage(title: string, msg: string): string {
        return `
          <p style="font-weight: bold; font-size: 1.2rem;">${title}</p>
          <p>${msg}</p>
      `;
    }
}
