<template>
  <div class="container">
    <h4>Create Theme</h4>

    <form @submit.prevent="themeCreate()">
      <div class="mb-3">
        <label for="inputThemeCreateThemeName" class="form-label">Theme Name</label>
        <input v-model="theme.themeName" type="themeName" class="form-control" id="inputThemeCreateThemeName">
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useThemeStore } from "@/stores/";
import type { ITheme } from "@/models/";

export default defineComponent({
  data() {
    return {
      theme: {} as ITheme
    }
  },
  methods: {
    themeCreate() {
      this.themeStore.apiCreate(this.theme);
      this.$emit('changePage', 'ThemeIndexComponent');
    },
    back() {
      this.$emit('changePage', 'ThemeIndexComponent');
    }
  },
  computed: {
    ...mapStores(useThemeStore)
  }
})
</script>