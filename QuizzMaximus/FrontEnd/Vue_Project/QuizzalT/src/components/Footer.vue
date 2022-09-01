<template>
  
    <footer class="footer text-center">
        <label class="toggle">
          <input type="checkbox" @change="$emit('toggle')" />
          <span class="toggler round"></span>
        </label>
    </footer>
 
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({})
</script>