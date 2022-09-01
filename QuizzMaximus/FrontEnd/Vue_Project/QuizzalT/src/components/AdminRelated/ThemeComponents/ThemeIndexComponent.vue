<template>
    <div class="container">
      <ModelTitle title="Theme Index" />
      <table class="table">
        <thead>
          <tr>
            <th>ThemeId</th>
            <th>Theme Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in themeStore.getThemes" :key="item.themeId">
            <td> {{ item.themeId }} </td>
            <td> {{ item.themeName }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="themeDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="themeEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="themeDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="themeCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useThemeStore } from "@/stores/";
import type { ITheme } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    themeDetails(theme: ITheme) {
      this.themeStore.setCurrentTheme(theme);
      this.$emit('changePage', 'ThemeDetailsComponent');
    },
    themeEdit(theme: ITheme) {
      this.themeStore.setCurrentTheme(theme);
      this.$emit('changePage', 'ThemeEditComponent');
    },
    themeDelete(theme: ITheme){
      this.themeStore.setCurrentTheme(theme);
      this.$emit('changePage', 'ThemeDeleteComponent');
    },
    themeCreate() {
      this.$emit('changePage', 'ThemeIndexComponent');
    }
  },
  computed:{
    ...mapStores(useThemeStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>