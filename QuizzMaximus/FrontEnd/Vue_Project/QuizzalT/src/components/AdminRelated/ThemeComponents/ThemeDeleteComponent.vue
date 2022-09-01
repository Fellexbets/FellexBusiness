<template>
  <div class="container">
    <h4>Delete Theme</h4>
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
    <p> Are you sure?</p>
    <button @click="themeDelete(theme.themeId)">Delete</button>
    <button @click="back()">Back</button>
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
    themeDelete(themeId: number){
        this.themeStore.apiDelete(themeId);
        this.$emit('changePage', 'ThemeIndexComponent');
      },
      back(){
        this.$emit('changePage', 'ThemeIndexComponent');
      }
  },
  mounted(){
    this.theme = this.themeStore.getCurrentTheme;
  },
  computed:{
    ...mapStores(useThemeStore)
  },
  components: {
  }
})
</script>