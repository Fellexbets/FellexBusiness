<template>
  <div class="container">
    <h4>Edit Theme</h4>

    <form @submit.prevent="themeUpdate()">
      <div class="mb-3">
        <label for="inputThemeEditThemeName" class="form-label">Current theme name: {{ themeToDisplayCurrent.themeName }}</label>
        <input v-model="theme.themeName" type="themeName" class="form-control" id="inputThemeEditThemeName">
      </div>

      <button type="submit">Update</button>
      <button @click="back(theme.themeId)">Back</button>
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
      theme: {} as ITheme,
      themeToDisplayCurrent: {} as ITheme
    }
  },
  methods: {
    themeUpdate() {
      this.themeStore.apiUpdate(this.theme);
      this.$router.push(`/themeDetails/${this.theme.themeId}`);
    },
    back(themeId: number) {
      this.$router.push(`/themeDetails/${themeId}`);
    }
  },
  mounted() {
    this.theme = this.themeStore.getCurrentTheme;
    this.themeToDisplayCurrent.themeName = this.theme.themeName;
  },
  computed: {
    ...mapStores(useThemeStore)
  }
})
</script>