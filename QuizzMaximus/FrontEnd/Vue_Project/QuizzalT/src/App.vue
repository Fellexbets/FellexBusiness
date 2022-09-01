<template>
  <div :class="mode">

    <RouterView />
    <Footer @toggle="toggle"></Footer>

  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { RouterLink, RouterView } from 'vue-router'
import Footer from "@/components/Footer.vue"

export default defineComponent({
  name: 'app',
  data() {
    return {
      mode: 'light'
    }
  },
  components:{
    Footer
  },
  methods: {
    toggle() {
      this.mode == "dark" ? this.mode = "light" : this.mode = "dark";
    }
  }
})
</script>







