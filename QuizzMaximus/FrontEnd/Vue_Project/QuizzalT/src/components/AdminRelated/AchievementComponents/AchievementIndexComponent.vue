<template>
    <div class="container">
      <ModelTitle title="Achievement Index" />
      <table class="table">
        <thead>
          <tr>
            <th>Theme Id</th>
            <th>User Id</th>
            <th>GainedPoints</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, themeId) in achievementStore.getAchievements" :key="themeId">
            <td> {{ item.themeId }} </td>
            <td> {{ item.userId  }} </td>
            <td> {{ item.gainedPoints }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="achievementDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="achievementEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="achievementDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="achievementCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useAchievementStore } from "@/stores/";
import type { IAchievement } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    achievementDetails(achievement: IAchievement) {
      this.achievementStore.setCurrentAchievement(achievement);
      this.$emit('changePage', 'AchievementDetailsComponent');
    },
    achievementEdit(achievement: IAchievement) {
      this.achievementStore.setCurrentAchievement(achievement);
      this.$emit('changePage', 'AchievementEditComponent');
    },
    achievementDelete(achievement: IAchievement){
      this.achievementStore.setCurrentAchievement(achievement);
      this.$emit('changePage', 'AchievementDeleteComponent');
    },
    achievementCreate() {
      this.$emit('changePage', 'AchievementCreateComponent');
    }
  },
  computed:{
    ...mapStores(useAchievementStore)
  },
  components: {
    ModelTitle
  },
  emits: ["changePage"]
})
</script>