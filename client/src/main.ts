import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import { config } from '@/config/config';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { createPinia } from 'pinia';
import ToastPlugin from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-default.css';
import { EntryService } from '@/services/EntryService'
import { NotificationService } from '@/services/NotificationService';

axios.defaults.baseURL = config.apiUrl;

const app = createApp(App)
app.component('font-awesome-icon', FontAwesomeIcon)

app.use(createPinia());
app.use(ToastPlugin);

const notificationService = new NotificationService()
const entryService = new EntryService(notificationService)

app.provide('notificationService', notificationService);
app.provide('entryService', entryService);

app.mount('#app')
