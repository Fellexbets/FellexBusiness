import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import NavBarComponent from './components/NavBarComponent.vue'
import './assets/css/base.css'

const app = createApp(App)

app.component('NavBarComponent', NavBarComponent)
app.use(createPinia())
app.use(router)

app.mount('#app')

