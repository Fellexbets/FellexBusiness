<template>
  <br />
  <h4>{{ title }}</h4>
  <hr />
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: ["title"]
})

</script>

