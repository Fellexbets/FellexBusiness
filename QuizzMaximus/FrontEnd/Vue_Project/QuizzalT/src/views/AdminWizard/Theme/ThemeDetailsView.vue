<template>
  <div class="container">
    <h4>Theme Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>ThemeId</th>
          <th>Theme Name</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ theme.themeId }} </td>
          <td> {{ theme.themeName }} </td>
        </tr>
      </tbody>
    </table>
    <button @click="themeEdit(theme)">Edit</button>
    <button @click="themeDelete(theme)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
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
    themeEdit(theme : ITheme){
      this.themeStore.setCurrentTheme(theme);
      this.$router.push(`/themeEdit/${theme.themeId}`)
    },
    themeDelete(theme: ITheme){
      this.themeStore.setCurrentTheme(theme);
      this.$router.push(`/themeDelete/${theme.themeId}`)
    },
    back() {
      this.$router.push(`/themeIndex`);
    },

  },
  mounted() {
   this.theme = this.themeStore.getCurrentTheme;
  },
  computed: {
    ...mapStores(useThemeStore)
  }
})
</script>